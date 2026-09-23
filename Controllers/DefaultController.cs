using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/[controller]")]
public class DefaultController : ControllerBase
{
  [HttpGet]
  public IActionResult Index([FromQuery] string? Name)
  {
    return Ok($"Default controller: {Name}");
  }

  [HttpGet("error")]
  public IActionResult Error()
  {
    throw new Exception("Error testing");
  }
}
