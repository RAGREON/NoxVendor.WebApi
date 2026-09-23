namespace NoxVendor.WebApi.Models;

public class Order
{
  public Guid Id { get; set; }

  public List<OrderItem> Items { get; set; } = [];

  public ApplicationUser User { get; set; } = null!;
  public Guid UserId { get; set; }

  public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
}
