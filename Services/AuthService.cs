using Microsoft.AspNetCore.Identity;
using NoxVendor.WebApi.DTOs;
using NoxVendor.WebApi.Models;

namespace NoxVendor.WebApi.Services;

public class AuthService(
  UserManager<ApplicationUser> userManager,
  TokenService tokenService
)
{
  private readonly UserManager<ApplicationUser> _userManager = userManager;
  private readonly TokenService _tokenService = tokenService;

  public async Task<string> RegisterUser(RegisterRequest request)
  {
    var user = await _userManager.FindByEmailAsync(request.Email);

    if (user != null)
      throw new ArgumentException($"{request.Email} is already registered");

    var newUser = new ApplicationUser()
    {
      UserName = request.Email,
      Email = request.Email,
      FirstName = request.FirstName,
      LastName = request.LastName,
    };

    var result = await _userManager.CreateAsync(newUser, request.Password);

    if (!result.Succeeded)
    {
      var errors = string.Join(", ", result.Errors.Select(e => e.Description));
      throw new ArgumentException($"User registration failed: ${errors}");
    }

    return newUser.Id;
  }

  public async Task<string> LoginUser(LoginRequest request)
  {
    var user =
      await _userManager.FindByEmailAsync(request.Email)
      ?? throw new ArgumentException($"Invalid email or password");

    bool isValidPassword = await _userManager.CheckPasswordAsync(
      user,
      request.Password
    );

    if (!isValidPassword)
    {
      throw new ArgumentException($"Invalid email or password");
    }

    var token = _tokenService.GenerateJwtToken(user.Id, user.Email!, ["User"]);

    return token;
  }
}
