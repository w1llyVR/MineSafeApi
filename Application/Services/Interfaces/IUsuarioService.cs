using Application.Dto.Usuario;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<RegistroResponse> CreateAsync(UsuarioRequestDto request);
        Task<RegistroResponse> ChangePasswordAsync(int codigoVerificacion, string email, string newPassword);
        Task<Response<UsuarioLoginResponseDto>> Login(string email, string password);
    }
}
