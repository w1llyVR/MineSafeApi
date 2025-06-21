using Application.Dto.FotoCondicion;
using Application.Dto.MedidaCorrectiva;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Ports.Output;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FotoCondicionService : IFotoCondicionService
    {
        private readonly IFotoCondicionRepository _fotoCondicionRepository;
        private readonly IMapper _mapper;

        public FotoCondicionService(IFotoCondicionRepository fotoCondicionRepository, IMapper mapper)
        {
            _fotoCondicionRepository = fotoCondicionRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<FotoCondicionResponseDto>>> GetAllAsync()
        {
            var result = await _fotoCondicionRepository.GetAllAsync();
            var dtoList = _mapper.Map<IEnumerable<FotoCondicionResponseDto>>(result.Data);

            return new Response<IEnumerable<FotoCondicionResponseDto>>
            {
                CodeError = result.CodeError,
                Msj = result.Msj,
                Data = dtoList
            };
        }

        public async Task<Response<IEnumerable<FotoCondicionResponseDto>>> GetByIdAsync(int id)
        {
            var result = await _fotoCondicionRepository.GetByIdAsync(id);
            var dtoList = _mapper.Map<IEnumerable<FotoCondicionResponseDto>>(result.Data);

            return new Response<IEnumerable<FotoCondicionResponseDto>>
            {
                CodeError = result.CodeError,
                Msj = result.Msj,
                Data = dtoList
            };
        }
        public async Task<RegistroResponse> CreateAsync(FotoCondicionRequestDto_Create request)
        {
            var requestMapper = _mapper.Map<FotoCondicion>(request);
            return await _fotoCondicionRepository.CreateAsync(requestMapper);
        }
        public async Task<RegistroResponse> UpdateAsync(FotoCondicionRequestDto_Update request)
        {
            var requestMapper = _mapper.Map<FotoCondicion>(request);
            return await _fotoCondicionRepository.UpdateAsync(requestMapper);
        }

        public async Task<RegistroResponse> DeleteAsync(int id)
        {
            return await _fotoCondicionRepository.DeleteAsync(id);
        }
    }
}
