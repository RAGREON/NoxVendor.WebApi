using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoxVendor.WebApi.Data;
using NoxVendor.WebApi.Models;

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

      options.UseSeeding(
        (context, _) =>
        {
          SeedRoles(context);
          SeedUsers(context);
        }
      );

      options.UseAsyncSeeding(
        async (context, _, cancellationToken) =>
        {
          await SeedRolesAsync(context, cancellationToken);
          await SeedUsersAsync(context, cancellationToken);
        }
      );
    });

    return services;
  }

  private static void SeedRoles(DbContext context)
  {
    // RoleName, Normalized RoleName
    List<(string, string)> roles = [("User", "USER"), ("Admin", "ADMIN")];
    var _roles = context.Set<IdentityRole<Guid>>();

    foreach (var (name, normalizedName) in roles)
    {
      if (!_roles.Any(r => r.Name == name))
      {
        _roles.Add(
          new IdentityRole<Guid> { Name = name, NormalizedName = normalizedName }
        );
      }
    }

    context.SaveChanges();
  }

  private static void SeedUsers(DbContext context)
  {
    // Role, ApplicationUser
    List<(string role, ApplicationUser user)> users =
    [
      (
        "Admin",
        new()
        {
          UserName = "admin",
          NormalizedUserName = "ADMIN",
          FirstName = "Salmon",
          LastName = "Fish",
          Email = "admin@gmail.com",
          NormalizedEmail = "ADMIN@GMAIL.COM",
          PasswordHash = "Admin@123",
          SecurityStamp = Guid.NewGuid().ToString(),
        }
      ),
    ];
    var _users = context.Set<ApplicationUser>();
    var _roles = context.Set<IdentityRole<Guid>>();
    var _userRoles = context.Set<IdentityUserRole<Guid>>();

    var passwordHasher = new PasswordHasher<ApplicationUser>();

    foreach (var (roleName, newUser) in users)
    {
      var user = _users.Single(u => u.UserName == newUser.UserName);

      if (user is null)
      {
        newUser.Id = Guid.NewGuid();
        newUser.PasswordHash = passwordHasher.HashPassword(
          newUser,
          newUser.PasswordHash!
        );
        _users.Add(newUser);
        user = newUser;
      }

      var role = _roles.Single(r => r.Name == roleName);

      _userRoles.Add(
        new IdentityUserRole<Guid> { UserId = user.Id, RoleId = role.Id }
      );
    }

    context.SaveChanges();
  }

  private static async Task SeedUsersAsync(
    DbContext context,
    CancellationToken cancellationToken
  )
  {
    // Role, ApplicationUser
    List<(string role, ApplicationUser user)> users =
    [
      (
        "Admin",
        new()
        {
          UserName = "admin",
          NormalizedUserName = "ADMIN",
          FirstName = "Salmon",
          LastName = "Fish",
          Email = "admin@gmail.com",
          NormalizedEmail = "ADMIN@GMAIL.COM",
          PasswordHash = "Admin@123",
          SecurityStamp = Guid.NewGuid().ToString(),
        }
      ),
    ];
    var _users = context.Set<ApplicationUser>();
    var _roles = context.Set<IdentityRole<Guid>>();
    var _userRoles = context.Set<IdentityUserRole<Guid>>();

    var passwordHasher = new PasswordHasher<ApplicationUser>();

    foreach (var (roleName, newUser) in users)
    {
      var user = await _users.SingleAsync(
        u => u.UserName == newUser.UserName,
        cancellationToken
      );

      if (user is null)
      {
        newUser.Id = Guid.NewGuid();
        newUser.PasswordHash = passwordHasher.HashPassword(
          newUser,
          newUser.PasswordHash!
        );
        await _users.AddAsync(newUser, cancellationToken);
        user = newUser;
      }

      var role = await _roles.SingleAsync(r => r.Name == roleName, cancellationToken);

      _userRoles.Add(
        new IdentityUserRole<Guid> { UserId = user.Id, RoleId = role.Id }
      );
    }

    await context.SaveChangesAsync(cancellationToken);
  }

  private static async Task SeedRolesAsync(
    DbContext context,
    CancellationToken cancellationToken
  )
  {
    // RoleName, Normalized RoleName
    List<(string, string)> roles = [("User", "USER"), ("Admin", "ADMIN")];
    var _roles = context.Set<IdentityRole<Guid>>();

    foreach (var (name, normalizedName) in roles)
    {
      if (!await _roles.AnyAsync(r => r.Name == name, cancellationToken))
      {
        await _roles.AddAsync(
          new IdentityRole<Guid> { Name = name, NormalizedName = normalizedName },
          cancellationToken
        );
      }
    }

    await context.SaveChangesAsync(cancellationToken);
  }
}
