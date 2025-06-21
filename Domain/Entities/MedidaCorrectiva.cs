using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MedidaCorrectiva
    {
        public int Id { get; set; }
        public int ReporteActaId { get; set; }
        public string Contenido { get; set; }
        public string Origen { get; set; }
        public DateTime FechaGeneracion { get; set; }
    }
}
