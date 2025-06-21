using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Output
{
    public interface IFotoCondicionRepository
    {
        Task<Response<IEnumerable<FotoCondicion>>> GetAllAsync();
        Task<Response<IEnumerable<FotoCondicion>>> GetByIdAsync(int id);
        Task<RegistroResponse> CreateAsync(FotoCondicion request);
        Task<RegistroResponse> UpdateAsync(FotoCondicion request);
        Task<RegistroResponse> DeleteAsync(int id);
    }
}
