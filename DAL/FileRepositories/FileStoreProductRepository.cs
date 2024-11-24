using Core.Interfaces;
using Core.Models;


namespace DAL.FileRepositories
{
    public class FileStoreProductRepository : IStoreProductRepository
    {
        private readonly string _filePath;

        public FileStoreProductRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task AddProductToStoreAsync(StoreProduct storeProduct)
        {
            var line = $"{storeProduct.StoreId},{storeProduct.ProductId},{storeProduct.Price},{storeProduct.Quantity}";
            await File.AppendAllTextAsync(_filePath, line + "\n");
        }

        public async Task<List<StoreProduct>> GetProductsInStoreAsync(int storeId)
        {
            if (!File.Exists(_filePath)) return new List<StoreProduct>();

            var lines = await File.ReadAllLinesAsync(_filePath);
            return lines
                .Select(line =>
                {
                    var parts = line.Split(',');
                    return new StoreProduct
                    {
                        StoreId = int.Parse(parts[0]),
                        ProductId = int.Parse(parts[1]),
                        Price = double.Parse(parts[2]),
                        Quantity = int.Parse(parts[3])
                    };
                })
                .Where(sp => sp.StoreId == storeId)
                .ToList();
        }

        public async Task<StoreProduct?> FindCheapestStoreAsync(string productName)
        {
            if (!File.Exists(_filePath)) return null;

            var products = await GetProductsInStoreAsync(0);
            return products
                .Where(sp => sp.Quantity > 0)
                .OrderBy(sp => sp.Price)
                .FirstOrDefault();
        }
    }
}