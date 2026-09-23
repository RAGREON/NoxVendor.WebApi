using Microsoft.EntityFrameworkCore;
using NoxVendor.WebApi.Data;
using NoxVendor.WebApi.DTOs;
using NoxVendor.WebApi.Models;

namespace NoxVendor.WebApi.Services;

public class OrderService(AppDbContext context)
{
  private readonly AppDbContext _context = context;

  public async Task<OrderDto> GetOrderByIdAsync(Guid id)
  {
    var order = await _context.Orders
      .Where(o => o.Id == id)
      .Select(o => new OrderDto(
        o.Id,
        o.Items.Select(i => new OrderItemDto(
            i.Id,
            new(
              i.Product.Id,
              i.Product.Name,
              i.Product.Description,
              i.Product.Price,
              i.Product.Stock,
              i.Product.Categories.Select(c => c.Name).ToList(),
              i.Product.Images.Select(i => i.Url!).ToList()
            ),
            i.Quantity,
            i.UnitPrice
          ))
          .ToList(),
        o.IssuedAt
      ))
      .FirstOrDefaultAsync();

    if (order is null)
    {
      throw new Exception($"Order {id} not found");
    }

    return order;
  }

  public async Task<Guid> CreateOrderAsync(CreateOrderDto dto)
  {
    var order = new Order { UserId = dto.UserId, Items = [] };

    foreach (var item in dto.Items)
    {
      var product = await _context.Products.FirstOrDefaultAsync(p =>
        p.Id == item.ProductId
      );

      if (product is null)
      {
        throw new Exception($"Product {item.ProductId} not found");
      }

      if (product.Stock < item.Quantity)
      {
        throw new Exception(
          $"Requested {item.Quantity} items, but {product.Stock} in stock"
        );
      }

      order.Items.Add(
        new OrderItem()
        {
          ProductId = item.ProductId,
          Quantity = item.Quantity,
          UnitPrice = item.UnitPrice,
        }
      );
    }

    _context.Orders.Add(order);
    await _context.SaveChangesAsync();

    return order.Id;
  }
}
