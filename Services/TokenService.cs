using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NoxVendor.WebApi.Services;

public class TokenService(IConfiguration config)
{
  private readonly IConfiguration _config = config;

  public string GenerateJwtToken(string userId, string email, IEnumerable<string> roles) 
  { 
    var secretkey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]!));

    var signingCredential = new SigningCredentials(
        secretkey, SecurityAlgorithms.HmacSha256);

    var claims = new List<Claim>
    {
      new(ClaimTypes.NameIdentifier, userId),
      new(ClaimTypes.Email, email)
    };

    foreach (var role in roles) 
    {
      claims.Add(new(ClaimTypes.Role, role));
    }

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Issuer = _config["Jwt:Issuer"],
      Audience = _config["Jwt:Audience"],
      Expires = DateTime.UtcNow.AddMinutes(2),
      SigningCredentials = signingCredential
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
  }
}
