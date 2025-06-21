using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.MedidaCorrectiva
{
    public class MedidaCorrectivaRequestDto_Create
    {
        public int ReporteActaId { get; set; }
        public string Contenido { get; set; }
        public string Origen { get; set; }
        public DateTime FechaGeneracion { get; set; }
    }

    public class MedidaCorrectivaRequestDto_Update
    {
        public int Id { get; set; }
        public int ReporteActaId { get; set; }
        public string Contenido { get; set; }
        public string Origen { get; set; }
        public DateTime FechaGeneracion { get; set; }
    }
}
