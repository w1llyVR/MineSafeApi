using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Output
{
    public interface IMedidaCorrectivaRepository
    {
        Task<Response<IEnumerable<MedidaCorrectiva>>> GetAllAsync();
        Task<Response<IEnumerable<MedidaCorrectiva>>> GetByIdAsync(int id);
        Task<RegistroResponse> CreateAsync(MedidaCorrectiva request);
        Task<RegistroResponse> UpdateAsync(MedidaCorrectiva request);
        Task<RegistroResponse> DeleteAsync(int id);
    }
}
