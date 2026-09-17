using Microsoft.EntityFrameworkCore;
using NoxVendor.WebApi.Models;

namespace NoxVendor.WebApi.Data
{
  public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
  {
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Product>()
        .HasMany(p => p.Images)
        .WithOne()
        .HasForeignKey(i => i.ProductId);
    }
  }
}