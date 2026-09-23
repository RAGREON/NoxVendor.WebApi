using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoxVendor.WebApi.Services;

namespace NoxVendor.WebApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Authorize(Roles = "Admin")]
public class RolesController(RoleService roleService) : ControllerBase
{
  private readonly RoleService _roleService = roleService;

  [HttpPost("")]
  public async Task<IActionResult> CreateRoleAsync(string role)
  {
    await _roleService.CreateRoleAsync(role);
    return Ok($"Role {role} has been created successfully");
  }

  [HttpGet("all")]
  public async Task<IActionResult> GetAllRolesAsync()
  {
    var roles = await _roleService.GetAllRolesAsync();
    return Ok(roles);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteRoleAsync([FromRoute] string id)
  {
    await _roleService.DeleteRoleByIdAsync(id);
    return Ok($"{id} deleted");
  }
}
