using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.TipoCondicion
{
    public class TipoCondicionRequestDto_Create
    {
        public string Nombre { get; set; }
    }
    public class TipoCondicionRequestDto_Update
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
