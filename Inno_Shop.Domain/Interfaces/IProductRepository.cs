using Inno_Shop.Domain.Entities;

namespace Inno_Shop.Domain.Interfaces;

public interface IProductRepository
{
    public Task CreateAsync(Product product);
    public Task UpdateAsync(Product product);
    public Task DeleteAsync(Product product);
    public Task<Product> GetByIdAsync(long id);
    public Task<List<Product>> GetAllAsync();
}