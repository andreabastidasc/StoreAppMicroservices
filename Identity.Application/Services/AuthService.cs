using Identity.Application.DTOs;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using BCrypt.Net;

namespace Identity.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterAsync(UserDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                return false;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Name = dto.Name
            };

            await _userRepository.AddAsync(user);
            return true;
        }

        public async Task<bool> LoginAsync(UserDto dto)
        {
            return await _userRepository.ValidateUserAsync(dto.Email, dto.Password);
        }
    }
}
