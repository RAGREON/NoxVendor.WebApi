using Microsoft.AspNetCore.Identity;
using NoxVendor.WebApi.Data;
using NoxVendor.WebApi.Models;

namespace NoxVendor.WebApi.Extensions;

public static class IdentityExtensions
{
  public static IServiceCollection AddIdentityServices(
    this IServiceCollection services
  )
  {
    services
      .AddIdentityCore<ApplicationUser>(options =>
      {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.User.RequireUniqueEmail = true;
      })
      .AddRoles<IdentityRole<Guid>>()
      .AddEntityFrameworkStores<AppDbContext>()
      .AddDefaultTokenProviders();

    return services;
  }
}
