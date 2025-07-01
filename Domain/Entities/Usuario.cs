using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
    }

    public class UsuarioLogin
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
