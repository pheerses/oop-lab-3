using Core.Models;

namespace Core.Interfaces
{
    public interface IStoreRepository
    {
        Task CreateStoreAsync(Store store);
        Task<List<Store>> GetAllStoresAsync();
        Task<Store?> GetStoreByIdAsync(int storeId);
    }

}
