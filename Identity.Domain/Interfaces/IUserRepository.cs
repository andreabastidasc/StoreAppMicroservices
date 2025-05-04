using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity user);
        Task<UserEntity?> GetByEmailAsync(string email);
        Task<bool> ValidateUserAsync(string email, string password);
    }
}
