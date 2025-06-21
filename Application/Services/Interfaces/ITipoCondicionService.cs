using Application.Dto.TipoCondicion;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ITipoCondicionService
    {
        Task<Response<IEnumerable<TipoCondicionResponseDto>>> GetAllAsync();
        Task<Response<IEnumerable<TipoCondicionResponseDto>>> GetByIdAsync(int id);
        Task<RegistroResponse> CreateAsync(TipoCondicionRequestDto_Create request);
        Task<RegistroResponse> UpdateAsync(TipoCondicionRequestDto_Update request);
        Task<RegistroResponse> DeleteAsync(int id);
    }
}
