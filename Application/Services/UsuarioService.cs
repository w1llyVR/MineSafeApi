using Application.Dto.TipoCondicion;
using Application.Dto.Usuario;
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
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            this._usuarioRepository = usuarioRepository;
            this._mapper = mapper;
        }

        public async Task<RegistroResponse> CreateAsync(UsuarioRequestDto request)
        {
            var requestMapper = _mapper.Map<Usuario>(request);
            return await _usuarioRepository.CreateAsync(requestMapper);
        }
        public async Task<RegistroResponse> ChangePasswordAsync(int codigoVerificacion, string email, string newPassword)
        {
            return await _usuarioRepository.ChangePasswordAsync(codigoVerificacion, email, newPassword);
        }
        public async Task<Response<UsuarioLoginResponseDto>> Login(string email, string password)
        {
            var result = await _usuarioRepository.Login(email, password);
            var dtoList = _mapper.Map<UsuarioLoginResponseDto>(result.Data);

            return new Response<UsuarioLoginResponseDto>
            {
                CodeError = result.CodeError,
                Msj = result.Msj,
                Data = dtoList
            };
        }
    }
}
