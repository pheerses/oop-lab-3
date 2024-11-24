using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StoreProduct> StoreProducts { get; set; }

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StoreProduct>()
                .HasKey(sp => new { sp.StoreId, sp.ProductId });

            modelBuilder.Entity<StoreProduct>()
                .HasOne<Store>()
                .WithMany()
                .HasForeignKey(sp => sp.StoreId);

            modelBuilder.Entity<StoreProduct>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(sp => sp.ProductId);
        }
    }

}
