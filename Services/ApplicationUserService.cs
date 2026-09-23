using Microsoft.AspNetCore.Identity;
using NoxVendor.WebApi.Models;

namespace NoxVendor.WebApi.Services;

public class ApplicationUserService(
  UserManager<ApplicationUser> userManager,
  RoleManager<IdentityRole<Guid>> roleManager
)
{
  private readonly UserManager<ApplicationUser> _userManager = userManager;
  private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;

  public async Task AddRoleAsync(Guid userId, Guid roleId)
  {
    var user = await _userManager.FindByIdAsync(userId.ToString());

    if (user is null)
    {
      throw new Exception($"User {userId} not found");
    }

    var role = await _roleManager.FindByIdAsync(roleId.ToString());

    if (role is null)
    {
      throw new Exception($"Role {roleId} not found");
    }

    await _userManager.AddToRoleAsync(user, role.Name!);
  }
}
