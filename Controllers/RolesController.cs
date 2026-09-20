using Microsoft.AspNetCore.Mvc;
using NoxVendor.WebApi.Services;

namespace NoxVendor.WebApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class RolesController(RoleService roleService) : ControllerBase
{
  private readonly RoleService _roleService = roleService;

  [HttpPost("create")]
  public async Task<IActionResult> CreateRoleAsync(string role)
  {
    try
    {
      await _roleService.CreateRoleAsync(role);
      return Ok($"Role {role} has been created successfully");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
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
    try
    {
      await _roleService.DeleteRoleByIdAsync(id);
      return Ok($"{id} deleted");
    }
    catch (Exception e)
    {
      return BadRequest($"Failed to delete role: {e.Message}");
    }
  }
}
