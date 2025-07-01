using Application.Dto.FotoCondicion;
using Application.Dto.ReporteActa;
using Application.Dto.TipoCondicion;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Ports.Output;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReporteActaService : IReporteActaService
    {
        private readonly IReporteActaRepository _reporteActaRepository;
        private readonly IFotoCondicionRepository _fotoCondicionRepository;
        private readonly IMapper _mapper;
        private readonly string fileSavePath = @"\\srvapplication\wwwroot\AlmacenamientoDocumentosWeb\MineSafe\";
        public ReporteActaService(IMapper mapper, IReporteActaRepository reporteActaRepository, IFotoCondicionRepository fotoCondicionRepository)
        {
            _mapper = mapper;
            _reporteActaRepository = reporteActaRepository;
            _fotoCondicionRepository = fotoCondicionRepository;
        }

        public async Task<Response<IEnumerable<ReporteActaResponseDto>>> GetAllAsync()
        {
            var result = await _reporteActaRepository.GetAllAsync();
            var dtoList = _mapper.Map<IEnumerable<ReporteActaResponseDto>>(result.Data);

            return new Response<IEnumerable<ReporteActaResponseDto>>
            {
                CodeError = result.CodeError,
                Msj = result.Msj,
                Data = dtoList
            };
        }

        public async Task<Response<IEnumerable<ReporteActaResponseDto>>> GetByIdAsync(int id)
        {
            var result = await _reporteActaRepository.GetByIdAsync(id);
            var photos = await _fotoCondicionRepository.GetByReporteIdAsync(id);
            var dtoPhotos = _mapper.Map<IEnumerable<FotoCondicionResponseDto>>(photos.Data);
            var dtoList = _mapper.Map<IEnumerable<ReporteActaResponseDto>>(result.Data);

            if(dtoPhotos != null)
                dtoList.FirstOrDefault().Fotos = dtoPhotos;

            return new Response<IEnumerable<ReporteActaResponseDto>>
            {
                CodeError = result.CodeError,
                Msj = result.Msj,
                Data = dtoList
            };
        }
        public async Task<RegistroResponse> CreateAsync(ReporteActaRequestDto_Create request)
        {
            var requestMapper = _mapper.Map<ReporteActa>(request);
            var reporteActaId =  await _reporteActaRepository.CreateAsync(requestMapper);



            foreach(var foto in request.Fotos)
            {
                var base64Data = foto.Imagen.Substring(foto.Imagen.IndexOf(',') + 1);
                var imageBytes = Convert.FromBase64String(base64Data);

                var tempFileName = "uploaded_image.png";
                var tempFilePath = Path.Combine(Path.GetTempPath(), tempFileName);
                await System.IO.File.WriteAllBytesAsync(tempFilePath, imageBytes);

                using (var stream = new FileStream(tempFilePath, FileMode.Open))
                {
                    var fileExtension = ".png";
                    var guid = Guid.NewGuid();
                    string dateTime = DateTime.Now.ToString("yyMMddHHmmss");
                    string uniqueId = guid.ToString("N") + dateTime;
                    string filename = $"{uniqueId}_foto{fileExtension}";
                    var filePath = Path.Combine(fileSavePath, filename);



                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                    filePath = "https://intranet.alpayana.com:1015/AlmacenamientoDocumentosWeb/MineSafe/" + filename;
                    foto.Ruta = filePath;

                }

                var requestFotoMapper = _mapper.Map<FotoCondicion>(foto);
                requestFotoMapper.ReporteActaId = reporteActaId;
                requestFotoMapper.Sincronizada = true;
                await _fotoCondicionRepository.CreateAsync(requestFotoMapper);

            }

            return new RegistroResponse
            {
                CodeError = HttpErrorCode.Success,
                Msj = "Reporte Acta creado exitosamente."
            };

        }
        public async Task<RegistroResponse> UpdateAsync(ReporteActaRequestDto_Update request)
        {
            var requestMapper = _mapper.Map<ReporteActa>(request);
            return await _reporteActaRepository.UpdateAsync(requestMapper);
        }

        public async Task<RegistroResponse> DeleteAsync(int id)
        {
            return await _reporteActaRepository.DeleteAsync(id);
        }
    }
}
