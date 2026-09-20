using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoxVendor.WebApi.DTOs;

namespace NoxVendor.WebApi.Services;

public class RoleService(RoleManager<IdentityRole> roleManager)
{
  private readonly RoleManager<IdentityRole> _roleManager = roleManager;

  public async Task CreateRoleAsync(string role)
  {
    bool exists = await _roleManager.RoleExistsAsync(role);

    if (!exists)
    {
      var result = await _roleManager.CreateAsync(new IdentityRole(role));

      if (!result.Succeeded)
      {
        throw new Exception(result.Errors.ToString());
      }
    }
  }

  public async Task<List<RoleDto>> GetAllRolesAsync()
  {
    var roles = await _roleManager
      .Roles.Select(r => new RoleDto(r.Id, r.Name, r.NormalizedName))
      .ToListAsync();

    return roles;
  }

  public async Task<bool> DeleteRoleByIdAsync(string id)
  {
    var role = await _roleManager.FindByIdAsync(id);

    if (role is null)
    {
      throw new Exception($"Role does not exist");
    }

    var result = await _roleManager.DeleteAsync(role);

    if (!result.Succeeded)
    {
      throw new Exception(result.Errors.ToString());
    }

    return true;
  }
}
