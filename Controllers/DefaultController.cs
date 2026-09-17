using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/[controller]")]
class DefaultController : ControllerBase {
  [HttpGet]
  public IActionResult Index() {
    return Ok("Default controller");
  }
}
