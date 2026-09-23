namespace NoxVendor.WebApi.DTOs;

public record OrderItemDto(
  Guid Id,
  ProductDto Product,
  int Quantity,
  decimal UnitPrice
);

public record OrderDto(
  Guid Id,
  List<OrderItemDto> Items,
  DateTime IssuedAt
);
