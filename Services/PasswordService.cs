using Microsoft.AspNetCore.Identity;
using NoxVendor.WebApi.Models;

namespace NoxVendor.WebApi.Services;

public class PasswordService(IPasswordHasher<ApplicationUser> passwordHasher)
{
  private readonly IPasswordHasher<ApplicationUser> _passwordHasher =
    passwordHasher;

  public string HashPassword(string password)
  {
    return _passwordHasher.HashPassword(new ApplicationUser(), password);
  }

  public bool VerifyPassword(string hashedPassword, string password)
  {
    var result = _passwordHasher.VerifyHashedPassword(
      new ApplicationUser(),
      hashedPassword,
      password
    );

    return result == PasswordVerificationResult.Success;
  }
}
