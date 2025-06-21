using Application.Dto.MedidaCorrectiva;
using Application.Dto.ReporteActa;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IMedidaCorrectivaService
    {
        Task<Response<IEnumerable<MedidaCorrectivaResponseDto>>> GetAllAsync();
        Task<Response<IEnumerable<MedidaCorrectivaResponseDto>>> GetByIdAsync(int id);
        Task<RegistroResponse> CreateAsync(MedidaCorrectivaRequestDto_Create request);
        Task<RegistroResponse> UpdateAsync(MedidaCorrectivaRequestDto_Update request);
        Task<RegistroResponse> DeleteAsync(int id);
    }
}
