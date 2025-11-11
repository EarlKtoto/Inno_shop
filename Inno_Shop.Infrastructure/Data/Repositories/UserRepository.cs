using Inno_Shop.Domain.Entities;
using Inno_Shop.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inno_Shop.Infrastructure.Data.Repositories;

public class UserRepository : IUserRrepository
{
    private Inno_ShopDbContext _context;
    
    public UserRepository(Inno_ShopDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(User user)
    {
        _context.Users.Add(user);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
    }

    public async Task<User> GetByIdAsync(long id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }
}