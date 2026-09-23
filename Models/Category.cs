namespace NoxVendor.WebApi.Models;

public class Category
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public required string Description { get; set; }

  public List<Product> Products { get; set; } = [];
}
