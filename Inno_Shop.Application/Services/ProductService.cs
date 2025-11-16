using Inno_Shop.Domain.Entities;
using Inno_Shop.Infrastructure.Data.Repositories;

namespace Application.Services;

public class ProductService
{
    readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void AddProduct(Product product)
    {
        if(_productRepository.GetByIdAsync(product.Id) != null) 
            throw new Exception("Product already exists");
        _productRepository.CreateAsync(product);
    }

    public void UpdateProduct(Product product)
    {
        if(_productRepository.GetByIdAsync(product.Id) == null)
            throw new Exception("Product not found");
        _productRepository.UpdateAsync(product);
    }

    public void DeleteProduct(Product product)
    {
        if(_productRepository.GetByIdAsync(product.Id) == null)
            throw new Exception("Product not found");
        _productRepository.DeleteAsync(product);
    }

    public Product GetProductInformation(int id)
    {
        if(_productRepository.GetByIdAsync(id) == null)
            throw new Exception("Product not found");
        return _productRepository.GetByIdAsync(id).Result;
    }

    public IEnumerable<Product> GetAllProductsInformation()
    {
        return _productRepository.GetAllAsync().Result;
    }
}