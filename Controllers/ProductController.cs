using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoxVendor.WebApi.Data;
using NoxVendor.WebApi.DTOs;
using NoxVendor.WebApi.Models;

namespace NoxVendor.WebApi.Controllers
{
  [ApiController]
  [Route("api/v1/[controller]")]
  public class ProductController(AppDbContext context) : ControllerBase
  {
    private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
    private readonly AppDbContext _context = context;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById([FromRoute] int id)
    {
      var product = await _context.Products
        .Where(p => p.Id == id)
        .Select(p => new
        {
          p.Id,
          p.Name,
          p.Description,
          p.Price,
          Images = p.Images.Select(i => i.Url).ToList()
        })
        .FirstOrDefaultAsync();

      return Ok(product);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto dto)
    {

      Product product = new()
      {
        Name = dto.Name,
        Description = dto.Description,
        Price = dto.Price,
        Stock = dto.Stock
      };      

      _context.Products.Add(product);
      await _context.SaveChangesAsync();

      foreach (var file in dto.Images)
      {
        if (file.Length > 0)
        {
          ProductImage productImage = new()
          {
            ProductId = product.Id
          };

          _context.ProductImages.Add(productImage);
          await _context.SaveChangesAsync();

          var filename = $"{productImage.Id}{Path.GetExtension(file.FileName)}";
          var filepath = Path.Combine(_uploadFolder, filename);

          using (var stream = new FileStream(filepath, FileMode.Create))
          {
            await file.CopyToAsync(stream);
          }

          productImage.Url = filename;

          await _context.SaveChangesAsync();
        }
      }

      return Ok(product.Id);
    }

  }
}
