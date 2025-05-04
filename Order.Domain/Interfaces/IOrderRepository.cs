using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Domain.Entities;

namespace Order.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderEntity>> GetAllAsync();
        Task<OrderEntity?> GetByIdAsync(Guid id);
        Task AddAsync(OrderEntity order);
        Task UpdateAsync(OrderEntity order);
        Task DeleteAsync(Guid id);
    }
}

