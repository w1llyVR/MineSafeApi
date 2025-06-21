using Application.Dto.MedidaCorrectiva;
using Application.Dto.ReporteActa;
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
    public class MedidaCorrectivaService : IMedidaCorrectivaService
    {
        private readonly IMedidaCorrectivaRepository _medidaCorrectivaRepository;
        private readonly IMapper _mapper;

        public MedidaCorrectivaService(IMedidaCorrectivaRepository medidaCorrectivaRepository, IMapper mapper)
        {
            _medidaCorrectivaRepository = medidaCorrectivaRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<MedidaCorrectivaResponseDto>>> GetAllAsync()
        {
            var result = await _medidaCorrectivaRepository.GetAllAsync();
            var dtoList = _mapper.Map<IEnumerable<MedidaCorrectivaResponseDto>>(result.Data);

            return new Response<IEnumerable<MedidaCorrectivaResponseDto>>
            {
                CodeError = result.CodeError,
                Msj = result.Msj,
                Data = dtoList
            };
        }

        public async Task<Response<IEnumerable<MedidaCorrectivaResponseDto>>> GetByIdAsync(int id)
        {
            var result = await _medidaCorrectivaRepository.GetByIdAsync(id);
            var dtoList = _mapper.Map<IEnumerable<MedidaCorrectivaResponseDto>>(result.Data);

            return new Response<IEnumerable<MedidaCorrectivaResponseDto>>
            {
                CodeError = result.CodeError,
                Msj = result.Msj,
                Data = dtoList
            };
        }
        public async Task<RegistroResponse> CreateAsync(MedidaCorrectivaRequestDto_Create request)
        {
            var requestMapper = _mapper.Map<MedidaCorrectiva>(request);
            return await _medidaCorrectivaRepository.CreateAsync(requestMapper);
        }
        public async Task<RegistroResponse> UpdateAsync(MedidaCorrectivaRequestDto_Update request)
        {
            var requestMapper = _mapper.Map<MedidaCorrectiva>(request);
            return await _medidaCorrectivaRepository.UpdateAsync(requestMapper);
        }

        public async Task<RegistroResponse> DeleteAsync(int id)
        {
            return await _medidaCorrectivaRepository.DeleteAsync(id);
        }


    }
}
