using Core.Interfaces;
using Core.Models;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace DAL.DatabaseRepositories
{
    public class DatabaseProductRepository : IProductRepository
    {
        private readonly ShopDbContext _dbContext;

        public DatabaseProductRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateProductAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _dbContext.Products.FindAsync(productId);
        }
    }

}
