using Core.Models;

namespace BLL.Services
{
    public interface IShopService
    {
        Task CreateStoreAsync(Store store);
        Task CreateProductAsync(Product product);
        Task AddProductToStoreAsync(int storeId, int productId, double price, int quantity);
        Task<List<Store>> GetStoresWithProductAsync(string productName);
        Task<Store?> FindCheapestStoreAsync(string productName);
    }

}
