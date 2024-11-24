using Core.Interfaces;
using Core.Models;


namespace DAL.FileRepositories
{

    public class FileProductRepository : IProductRepository
    {
        private readonly string _filePath;

        public FileProductRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task CreateProductAsync(Product product)
        {
            var line = $"{product.Id},{product.Name}";
            await File.AppendAllTextAsync(_filePath, line + "\n");
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            if (!File.Exists(_filePath)) return new List<Product>();

            var lines = await File.ReadAllLinesAsync(_filePath);
            return lines.Select(line =>
            {
                var parts = line.Split(',');
                return new Product
                {
                    Id = int.Parse(parts[0]),
                    Name = parts[1]
                };
            }).ToList();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            var products = await GetAllProductsAsync();
            return products.FirstOrDefault(p => p.Id == productId);
        }
    }
}