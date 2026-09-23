namespace NoxVendor.WebApi.Models;

public class ProductImage
{
  public Guid Id { get; set; }
  public string? Url { get; set; }

  public Guid ProductId { get; set; }
}
