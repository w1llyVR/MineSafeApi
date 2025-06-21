using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Output
{
    public interface IReporteActaRepository
    {
        Task<Response<IEnumerable<ReporteActa>>> GetAllAsync();
        Task<Response<IEnumerable<ReporteActa>>> GetByIdAsync(int id);
        Task<int> CreateAsync(ReporteActa request);
        Task<RegistroResponse> UpdateAsync(ReporteActa request);
        Task<RegistroResponse> DeleteAsync(int id);
    }
}
