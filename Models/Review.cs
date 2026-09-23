namespace NoxVendor.WebApi.Models;

public class Review
{
  public Guid Id { get; set; }
  public short Rating { get; set; }
  public string? Description { get; set; }

  public ApplicationUser User { get; set; } = null!;
  public Guid UserId { get; set; }

  public Product Product { get; set; } = null!;
  public Guid ProductId { get; set; }
}
