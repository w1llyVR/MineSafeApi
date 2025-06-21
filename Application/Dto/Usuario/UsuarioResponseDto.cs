using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Usuario
{
    public class UsuarioResponseDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
    
    }
    
    public class UsuarioLoginResponseDto
    {
        public string Nombres { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
