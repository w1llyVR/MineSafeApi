using Application.Dto.ReporteActa;
using Application.Dto.TipoCondicion;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IReporteActaService
    {
        Task<Response<IEnumerable<ReporteActaResponseDto>>> GetAllAsync();
        Task<Response<IEnumerable<ReporteActaResponseDto>>> GetByIdAsync(int id);
        Task<RegistroResponse> CreateAsync(ReporteActaRequestDto_Create request);
        Task<RegistroResponse> UpdateAsync(ReporteActaRequestDto_Update request);
        Task<RegistroResponse> DeleteAsync(int id);
    }
}
