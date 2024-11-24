using Core.Models;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task CreateProductAsync(Product product);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int productId);
    }
}
