using Core.Interfaces;
using Core.Models;



namespace DAL.FileRepositories
{
    public class FileStoreRepository : IStoreRepository
    {
        private readonly string _filePath;

        public FileStoreRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task CreateStoreAsync(Store store)
        {
            var line = $"{store.Id},{store.Name},{store.Address}";
            await File.AppendAllTextAsync(_filePath, line + "\n");
        }

        public async Task<List<Store>> GetAllStoresAsync()
        {
            if (!File.Exists(_filePath)) return new List<Store>();

            var lines = await File.ReadAllLinesAsync(_filePath);
            return lines.Select(line =>
            {
                var parts = line.Split(',');
                return new Store
                {
                    Id = int.Parse(parts[0]),
                    Name = parts[1],
                    Address = parts[2]
                };
            }).ToList();
        }

        public async Task<Store?> GetStoreByIdAsync(int storeId)
        {
            var stores = await GetAllStoresAsync();
            return stores.FirstOrDefault(s => s.Id == storeId);
        }
    }
}