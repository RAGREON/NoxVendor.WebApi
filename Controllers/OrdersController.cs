using Microsoft.AspNetCore.Mvc;
using NoxVendor.WebApi.DTOs;
using NoxVendor.WebApi.Services;

namespace NoxVendor.WebApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class OrdersController(OrderService orderService) : ControllerBase
{
  public OrderService _orderService = orderService;

  [HttpGet("{id}")]
  public async Task<IActionResult> GetOrderAsync([FromRoute] Guid id)
  {
    var orderDto = await _orderService.GetOrderByIdAsync(id);
    return Ok(orderDto);
  }

  [HttpPost]
  public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderDto dto)
  {
    var orderId = await _orderService.CreateOrderAsync(dto);
    return Ok(orderId);
  }
}
