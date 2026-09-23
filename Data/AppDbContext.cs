using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoxVendor.WebApi.Models;

namespace NoxVendor.WebApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
  : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
{
  public DbSet<Product> Products => Set<Product>();
  public DbSet<ProductImage> ProductImages => Set<ProductImage>();
  public DbSet<Review> Reviews => Set<Review>();
  public DbSet<Category> Categories => Set<Category>();
  public DbSet<Order> Orders => Set<Order>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
  }
}
