using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoxVendor.WebApi.DTOs;
using NoxVendor.WebApi.Services;

namespace NoxVendor.WebApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class AuthController(AuthService authService) : ControllerBase
{
  private readonly AuthService _authService = authService;

  [AllowAnonymous]
  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginRequest request)
  {
    var token = await _authService.LoginUser(request);
    return Ok(token);
  }

  [AllowAnonymous]
  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterRequest request)
  {
    var userId = await _authService.RegisterUser(request);
    return Ok(new { userId });
  }
}
