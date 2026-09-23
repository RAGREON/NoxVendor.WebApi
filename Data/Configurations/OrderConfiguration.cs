using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoxVendor.WebApi.Models;

namespace NoxVendor.WebApi.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder
      .HasMany(o => o.Items)
      .WithOne(i => i.Order);
  }
}
