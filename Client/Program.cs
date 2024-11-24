using BLL.Services;
using Core.Interfaces;
using Core.Models;
using DAL;
using DAL.DatabaseRepositories;
using DAL.FileRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyShop.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var serviceProvider = ConfigureServices(configuration);

            var shopService = serviceProvider.GetService<IShopService>();

            await DemonstrateShopFunctionality(shopService);
        }

        private static ServiceProvider ConfigureServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            string dalImplementation = configuration["DAL:Implementation"];

            if (dalImplementation == "Database")
            {
                services.AddScoped<IStoreRepository, DatabaseStoreRepository>();
                services.AddScoped<IProductRepository, DatabaseProductRepository>();
                services.AddScoped<IStoreProductRepository, DatabaseStoreProductRepository>();
                services.AddDbContext<ShopDbContext>(options =>
                    options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            }
            else if (dalImplementation == "File")
            {
                services.AddScoped<IStoreRepository>(provider => new FileStoreRepository("stores.csv"));
                services.AddScoped<IProductRepository>(provider => new FileProductRepository("products.csv"));
                services.AddScoped<IStoreProductRepository>(provider => new FileStoreProductRepository("store_products.csv"));
            }
            else
            {
                throw new Exception("Invalid DAL implementation specified in appsettings.json.");
            }

            services.AddScoped<IShopService, ShopService>();

            return services.BuildServiceProvider();
        }

        private static async Task DemonstrateShopFunctionality(IShopService shopService)
        {
            Console.WriteLine("=== Магазинная система ===");

            var store = new Store { Id = 1, Name = "Магазин 1", Address = "Улица Ленина, 10" };
            await shopService.CreateStoreAsync(store);
            Console.WriteLine($"Создан магазин: {store.Name}.");

            var product = new Product { Id = 1, Name = "Мороженое" };
            await shopService.CreateProductAsync(product);
            Console.WriteLine($"Создан продукт: {product.Name}.");

            await shopService.AddProductToStoreAsync(1, 1, 50, 100);
            Console.WriteLine($"Товар '{product.Name}' добавлен в магазин '{store.Name}' с ценой 50 рублей и количеством 100.");

            var cheapestStore = await shopService.FindCheapestStoreAsync("Мороженое");
            if (cheapestStore != null)
            {
                Console.WriteLine($"Самый дешевый магазин для 'Мороженое': {cheapestStore.Name}.");
            }
        }
    }
}
