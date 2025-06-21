using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Output
{
    public interface ITipoCondicionRepository
    {
        Task<Response<IEnumerable<TipoCondicion>>> GetAllAsync();
        Task<Response<IEnumerable<TipoCondicion>>> GetByIdAsync(int id);
        Task<RegistroResponse> CreateAsync(TipoCondicion tipoCondicion);
        Task<RegistroResponse> UpdateAsync(TipoCondicion tipoCondicion);
        Task<RegistroResponse> DeleteAsync(int id);
    }
}
