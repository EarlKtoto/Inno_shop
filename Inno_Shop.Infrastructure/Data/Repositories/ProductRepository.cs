using Inno_Shop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inno_Shop.Infrastructure.Data.Repositories;

public class ProductRepository
{
    private Inno_ShopDbContext _context;

    public ProductRepository(Inno_ShopDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Product product)
    {
        _context.Products.Add(product);
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);   
    }

    public async Task<Product> GetByIdAsync(long id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }
}