using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoxVendor.WebApi.Data;

namespace NoxVendor.WebApi.Extensions;

public static class DatabaseExtensions
{
  public static IServiceCollection AddDatabase(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    services.AddDbContext<AppDbContext>(options =>
    {
      options.UseNpgsql(configuration.GetConnectionString("Default"));
      options.UseAsyncSeeding(
        async (context, _, cancellationToken) =>
        {
          await SeedRolesAsync(context, cancellationToken);
        }
      );
    });

    return services;
  }

  private static async Task SeedRolesAsync(
    DbContext context,
    CancellationToken cancellationToken
  )
  {
    List<string> initialRoles = ["User", "Admin"];
    var roles = context.Set<IdentityRole>();

    foreach (var initialRole in initialRoles)
    {
      if (!await roles.AnyAsync(r => r.Name == initialRole, cancellationToken))
      {
        await roles.AddAsync(new IdentityRole(initialRole), cancellationToken);
      }
    }

    await context.SaveChangesAsync(cancellationToken);
  }
}
