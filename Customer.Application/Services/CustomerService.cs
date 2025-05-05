using Customer.Application.DTOs;
using Customer.Domain.Entities;
using Customer.Domain.Interfaces;

namespace Customer.Application.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _repository.GetAllAsync();

            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                RegistrationDate = c.RegistrationDate
            });
        }

        public async Task<CustomerDto?> GetByIdAsync(Guid id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                RegistrationDate = customer.RegistrationDate
            };
        }

        public async Task<CustomerDto> CreateAsync(CustomerDto dto)
        {
            var entity = new CustomerEntity
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Address = dto.Address,
                Phone = dto.Phone,
                RegistrationDate = DateTime.UtcNow
            };

            await _repository.AddAsync(entity);

            return new CustomerDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Address = entity.Address,
                Phone = entity.Phone
            };
        }


        public async Task UpdateAsync(CustomerDto dto)
        {
            var entity = new CustomerEntity
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                RegistrationDate = dto.RegistrationDate
            };

            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
