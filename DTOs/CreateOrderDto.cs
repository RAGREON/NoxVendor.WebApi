namespace NoxVendor.WebApi.DTOs;

public record CreateOrderItemDetail(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice);

public record CreateOrderDto(
    Guid UserId,
    List<CreateOrderItemDetail> Items);
