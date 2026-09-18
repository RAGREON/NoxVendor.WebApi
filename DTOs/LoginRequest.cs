namespace NoxVendor.WebApi.DTOs;

public record LoginRequest(
    string Email, 
    string Password);
