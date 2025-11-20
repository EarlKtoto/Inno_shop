using Inno_Shop.Domain.Entities;
using Inno_Shop.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inno_Shop.Infrastructure.Data.Repositories;

public class UserRepository : IUserRrepository
{
    readonly Inno_ShopDbContext _context;
    
    public UserRepository(Inno_ShopDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(User user)
    {
        user.Id = _context.Users.Max(x => x.Id) + 1;
        _context.Users.Add(user);
        await _context.SaveChangesAsync(); 
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetByIdAsync(long id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> GetByConfirmationTokenAsync(string token)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.EmailConfirmationToken == token);
    }

    public async Task<User> GetByPasswordResetTokenAsync(string token)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == token);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }
}