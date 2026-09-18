using Microsoft.AspNetCore.Identity;
using NoxVendor.WebApi.Models;

namespace NoxVendor.WebApi.Services;

public class ApplicationUserService(UserManager<ApplicationUser> userManager)
{
  private readonly UserManager<ApplicationUser> _userManager = userManager;
}
