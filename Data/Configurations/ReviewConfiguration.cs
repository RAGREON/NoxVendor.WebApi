using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoxVendor.WebApi.Models;

namespace NoxVendor.WebApi.Data.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
  public void Configure(EntityTypeBuilder<Review> builder)
  {
    builder
      .HasOne(r => r.User)
      .WithMany()
      .HasForeignKey(r => r.UserId);
  }
}
