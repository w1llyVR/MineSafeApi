using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Usuario
{
    public class UsuarioRequestDto
    {
        public string Nombres { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
