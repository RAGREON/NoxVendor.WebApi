using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoxVendor.WebApi.Models;

namespace NoxVendor.WebApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
  : IdentityDbContext<ApplicationUser>(options)
{
  public DbSet<Product> Products => Set<Product>();
  public DbSet<ProductImage> ProductImages => Set<ProductImage>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
  }
}
