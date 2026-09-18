using Microsoft.AspNetCore.Mvc;
using NoxVendor.WebApi.DTOs;
using NoxVendor.WebApi.Services;

namespace NoxVendor.WebApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class AuthController(AuthService authService) : ControllerBase
{
  private readonly AuthService _authService = authService;
  
  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginRequest request)
  {
    try 
    {
      var token = await _authService.LoginUser(request);
      return Ok(new { token });
    }
    catch (Exception e)
    {
      return Unauthorized(e.Message);
    }
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterRequest request)
  {
    try 
    {
      var userId = await _authService.RegisterUser(request);
      return Ok(new { userId });
    }
    catch (Exception e)
    {
      return Conflict(e.Message);
    }
  }
}
