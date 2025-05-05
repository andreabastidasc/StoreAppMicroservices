using Identity.Application.Clients;
using Identity.Application.DTOs;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using BCrypt.Net;

namespace Identity.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly CustomerClient _customerClient;

        public AuthService(IUserRepository userRepository, CustomerClient customerClient)
        {
            Console.WriteLine("AuthService constructor called");

            _userRepository = userRepository;
            _customerClient = customerClient;
        }

        public async Task<bool> RegisterAsync(UserDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                return false;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var createdCustomer = await _customerClient.CreateCustomerAsync(new CustomerDto
            {
                Name = dto.Name,
                Email = dto.Email,
                Address = "Default Address",
                Phone = "Default Phone"
            });

            if (createdCustomer == null)
                throw new Exception("Error creating customer.");

            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Name = dto.Name,
                CustomerId = createdCustomer.Id
            };

            await _userRepository.AddAsync(user);

            return true;
        }

        public async Task<UserEntity?> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null) return null;

            var isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            return isValid ? user : null;
        }
    }
}
