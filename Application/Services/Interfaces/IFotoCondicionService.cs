using Application.Dto.FotoCondicion;
using Application.Dto.MedidaCorrectiva;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IFotoCondicionService
    {
        Task<Response<IEnumerable<FotoCondicionResponseDto>>> GetAllAsync();
        Task<Response<IEnumerable<FotoCondicionResponseDto>>> GetByIdAsync(int id);
        Task<RegistroResponse> CreateAsync(FotoCondicionRequestDto_Create request);
        Task<RegistroResponse> UpdateAsync(FotoCondicionRequestDto_Update request);
        Task<RegistroResponse> DeleteAsync(int id);
    }
}
