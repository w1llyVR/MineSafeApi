using Application.Dto.FotoCondicion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.ReporteActa
{
    public class ReporteActaResponseDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Observaciones { get; set; }
        
        public IEnumerable<FotoCondicionResponseDto> Fotos { get; set; }
    }
}
