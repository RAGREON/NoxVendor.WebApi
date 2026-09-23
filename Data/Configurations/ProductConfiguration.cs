using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoxVendor.WebApi.Models;

namespace NoxVendor.WebApi.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder
      .HasMany(p => p.Images)
      .WithOne()
      .HasForeignKey(i => i.ProductId);

    builder
      .HasMany(p => p.Categories)
      .WithMany(c => c.Products);

    builder
      .HasMany(p => p.Reviews)
      .WithOne(r => r.Product)
      .HasForeignKey(r => r.ProductId);
  }
}
