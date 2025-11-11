using Inno_Shop.Domain.Entities;

namespace Inno_Shop.Domain.Interfaces;

public interface IUserRrepository
{
    public Task CreateAsync(User user);
    public Task UpdateAsync(User user);
    public Task DeleteAsync(User user);
    public Task<User> GetByIdAsync(long id);
    public Task<List<User>> GetAllAsync();
}