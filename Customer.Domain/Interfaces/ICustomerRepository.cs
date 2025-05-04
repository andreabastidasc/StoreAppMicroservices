using Customer.Domain.Entities;

namespace Customer.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerEntity>> GetAllAsync();
        Task<CustomerEntity?> GetByIdAsync(Guid id);
        Task AddAsync(CustomerEntity customer);
        Task UpdateAsync(CustomerEntity customer);
        Task DeleteAsync(Guid id);
    }
}

