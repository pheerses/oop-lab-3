using Core.Models;

namespace Core.Interfaces
{
    public interface IStoreProductRepository
    {
        Task AddProductToStoreAsync(StoreProduct storeProduct);
        Task<List<StoreProduct>> GetProductsInStoreAsync(int storeId);
        Task<StoreProduct?> FindCheapestStoreAsync(string productName);
    }

}
