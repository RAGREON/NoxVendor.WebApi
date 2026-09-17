using Microsoft.AspNetCore.SignalR;

namespace NoxVendor.WebApi.DTOs;

public record CreateProductDto(
  string Name,
  string Description,
  decimal Price,
  int Stock,
  List<IFormFile> Images
);