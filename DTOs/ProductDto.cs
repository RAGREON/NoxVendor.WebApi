namespace NoxVendor.WebApi.DTOs;

public record ProductDto(
  Guid Id,
  string? Name,
  string? Description,
  decimal Price,
  int Stock,
  List<string> Categories,
  List<string> Images
);
