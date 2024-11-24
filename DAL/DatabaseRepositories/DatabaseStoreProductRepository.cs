using Core.Interfaces;
using Core.Models;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace DAL.DatabaseRepositories
{
    public class DatabaseStoreProductRepository : IStoreProductRepository
    {
        private readonly ShopDbContext _dbContext;

        public DatabaseStoreProductRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProductToStoreAsync(StoreProduct storeProduct)
        {
            _dbContext.StoreProducts.Add(storeProduct);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<StoreProduct>> GetProductsInStoreAsync(int storeId)
        {
            return await _dbContext.StoreProducts
                .Where(sp => sp.StoreId == storeId)
                .ToListAsync();
        }

        public async Task<StoreProduct?> FindCheapestStoreAsync(string productName)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == productName);
            if (product == null) return null;

            return await _dbContext.StoreProducts
                .Where(sp => sp.ProductId == product.Id && sp.Quantity > 0)
                .OrderBy(sp => sp.Price)
                .FirstOrDefaultAsync();
        }
    }
}