using Inno_Shop.Domain.Entities;
using Inno_Shop.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inno_Shop.Infrastructure.Data.Repositories;

public class ProductRepository : IProductRepository
{
    readonly Inno_ShopDbContext _context;

    public ProductRepository(Inno_ShopDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Product product)
    {
        product.Id = _context.Products.Max(p => p.Id) + 1;
        product.CreatedOn = DateTimeOffset.UtcNow;
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);   
        await _context.SaveChangesAsync();
    }

    public async Task<Product> GetByIdAsync(long id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }
}