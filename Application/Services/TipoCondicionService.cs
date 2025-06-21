using Application.Dto.TipoCondicion;
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
    public class TipoCondicionService : ITipoCondicionService
    {
        private readonly ITipoCondicionRepository _tipoCondicionRepository;
        private readonly IMapper _mapper;

        public TipoCondicionService(ITipoCondicionRepository tipoCondicionRepository, IMapper mapper)
        {
            _tipoCondicionRepository = tipoCondicionRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<TipoCondicionResponseDto>>> GetAllAsync()
        {
            var result = await _tipoCondicionRepository.GetAllAsync();
            var dtoList = _mapper.Map<IEnumerable<TipoCondicionResponseDto>>(result.Data);

            return new Response<IEnumerable<TipoCondicionResponseDto>>
            {
                CodeError = result.CodeError,
                Msj = result.Msj,
                Data = dtoList
            };
        }

        public async Task<Response<IEnumerable<TipoCondicionResponseDto>>> GetByIdAsync(int id)
        {
            var result = await _tipoCondicionRepository.GetByIdAsync(id);
            var dtoList = _mapper.Map<IEnumerable<TipoCondicionResponseDto>>(result.Data);

            return new Response<IEnumerable<TipoCondicionResponseDto>>
            {
                CodeError = result.CodeError,
                Msj = result.Msj,
                Data = dtoList
            };
        }
        public async Task<RegistroResponse> CreateAsync(TipoCondicionRequestDto_Create request)
        {
            var requestMapper = _mapper.Map<TipoCondicion>(request);
            return await _tipoCondicionRepository.CreateAsync(requestMapper);
        }
        public async Task<RegistroResponse> UpdateAsync(TipoCondicionRequestDto_Update request)
        {
            var requestMapper = _mapper.Map<TipoCondicion>(request);
            return await _tipoCondicionRepository.UpdateAsync(requestMapper);
        }

        public async Task<RegistroResponse> DeleteAsync(int id)
        {
            return await _tipoCondicionRepository.DeleteAsync(id);
        }
     
    }
}
