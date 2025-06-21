using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Output
{
    public interface IUsuarioRepository
    {
        Task<RegistroResponse> CreateAsync(Usuario request);
        Task<RegistroResponse> ChangePasswordAsync(int codigoVerificacion, string email, string newPassword);
        Task<Response<UsuarioLogin>> Login(string email, string password);
    }
}
