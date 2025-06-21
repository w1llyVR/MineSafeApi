using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.FotoCondicion
{
    public class FotoCondicionRequestDto_Create
    {
        //public int ReporteActaId { get; set; }
        public int TipoCondicionId { get; set; }
        public string? Ruta { get; set; }
        public string NivelRiesgo { get; set; }
        //public bool Sincronizada { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCaptura { get; set; }
        public string Imagen { get; set; }

    }

    public class FotoCondicionRequestDto_Update
    {
        public int Id { get; set; }
        public int ReporteActaId { get; set; }
        public int TipoCondicionId { get; set; }
        public string Ruta { get; set; }
        public string NivelRiesgo { get; set; }
        public bool Sincronizada { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCaptura { get; set; }
    }
}
