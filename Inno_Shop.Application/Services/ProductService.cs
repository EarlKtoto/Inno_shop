using Inno_Shop.Domain.Entities;
using Inno_Shop.Infrastructure.Data.Repositories;

namespace Application.Services;

public class ProductService
{
    readonly ProductRepository _productRepository;
    readonly ProductValidator _productValidator;

    public ProductService(ProductRepository productRepository, ProductValidator productValidator)
    {
        _productRepository = productRepository;
        _productValidator = productValidator;
    }

    public async Task AddProduct(Product product)
    {
        _productValidator.Validate(product);
        if(_productRepository.GetByIdAsync(product.Id) != null) 
            throw new Exception("Product already exists");
        await _productRepository.CreateAsync(product);
    }

    public async Task UpdateProduct(Product product)
    {
        _productValidator.Validate(product);
        if(_productRepository.GetByIdAsync(product.Id) == null)
            throw new Exception("Product not found");
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProduct(Product product)
    {
        if(_productRepository.GetByIdAsync(product.Id) == null)
            throw new Exception("Product not found");
        await _productRepository.DeleteAsync(product);
    }

    public async Task<List<Product>> SearchProductsByPriceFilterAsync(decimal minPrice, decimal maxPrice)
    {
        var products = await _productRepository.GetAllAsync();
        List<Product> searchedProducts = new List<Product>();

        foreach (var product in products)
        {
            if(!product.User.IsActive) continue;
            if(product.Price >= minPrice && product.Price <= maxPrice)
                searchedProducts.Add(product);
        }
        
        return searchedProducts;
    }
    
    public async Task<List<Product>> SearchProductsAsync(string line)
    {
        var products = await _productRepository.GetAllAsync();
        List<Product> searchedProducts = new List<Product>();

        foreach (var product in products)
        {
            if(!product.User.IsActive) continue;
            if(product.Name.ToLower().Contains(line.ToLower()))
                searchedProducts.Add(product);
        }
        
        return searchedProducts;
    }
    
    public async Task<Product> GetProductInformation(int id)
    {
        if(_productRepository.GetByIdAsync(id) == null)
            throw new Exception("Product not found");
        return _productRepository.GetByIdAsync(id).Result;
    }

    public async Task<List<Product>> GetAllProductsInformationAsync()
    {
        return _productRepository.GetAllAsync().Result;
    }
}