using Core.Interfaces;
using Core.Models;

namespace BLL.Services
{
    public class ShopService : IShopService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStoreProductRepository _storeProductRepository;

        public ShopService(
            IStoreRepository storeRepository,
            IProductRepository productRepository,
            IStoreProductRepository storeProductRepository)
        {
            _storeRepository = storeRepository;
            _productRepository = productRepository;
            _storeProductRepository = storeProductRepository;
        }

        public async Task CreateStoreAsync(Store store) => await _storeRepository.CreateStoreAsync(store);
        public async Task CreateProductAsync(Product product) => await _productRepository.CreateProductAsync(product);
        public async Task AddProductToStoreAsync(int storeId, int productId, double price, int quantity)
        {
            var storeProduct = new StoreProduct
            {
                StoreId = storeId,
                ProductId = productId,
                Price = price,
                Quantity = quantity
            };
            await _storeProductRepository.AddProductToStoreAsync(storeProduct);
        }

        public async Task<Store?> FindCheapestStoreAsync(string productName) =>
            (await _storeProductRepository.FindCheapestStoreAsync(productName))?.StoreId is int storeId
            ? await _storeRepository.GetStoreByIdAsync(storeId)
            : null;

        public Task<List<Store>> GetStoresWithProductAsync(string productName)
        {
            throw new NotImplementedException();
        }
    }

}
