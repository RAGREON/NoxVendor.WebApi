namespace NoxVendor.WebApi.Models;

public class OrderItem
{
  public Guid Id { get; set; }

  public Order Order { get; set; } = null!;
  public Guid OrderId { get; set; }

  public Product Product { get; set; } = null!;
  public Guid ProductId { get; set; }

  public int Quantity { get; set; }
  public decimal UnitPrice { get; set; }
}
