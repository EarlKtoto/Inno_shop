using Inno_Shop.Domain.Entities;

namespace Inno_Shop.Domain.Interfaces;

public interface IUserRepository
{
    public Task CreateAsync(User user);
    public Task UpdateAsync(User user);
    public Task DeleteAsync(User user);
    public Task<User> GetByIdAsync(long id);
    public Task<List<User>> GetAllAsync();
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByConfirmationTokenAsync(string token);
    Task<User> GetByPasswordResetTokenAsync(string token);
}