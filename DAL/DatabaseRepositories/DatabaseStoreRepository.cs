using Core.Interfaces;
using Core.Models;
using DAL;
using Microsoft.EntityFrameworkCore;


namespace DAL.DatabaseRepositories
{
    public class DatabaseStoreRepository : IStoreRepository
    {
        private readonly ShopDbContext _dbContext;

        public DatabaseStoreRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateStoreAsync(Store store)
        {
            _dbContext.Stores.Add(store);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Store>> GetAllStoresAsync()
        {
            return await _dbContext.Stores.ToListAsync();
        }

        public async Task<Store?> GetStoreByIdAsync(int storeId)
        {
            return await _dbContext.Stores.FindAsync(storeId);
        }
    }
}