using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NoxVendor.WebApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
  options.AddPolicy(
    "AllowFrontendApp",
    policy =>
    {
      policy
        .WithOrigins("http://localhost:3000") // Target allowed domains
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    }
  );
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
{
  options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

app.UseRouting();

app.UseCors("AllowFrontendApp");

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.UseSwaggerUI(options =>
  {
    options.SwaggerEndpoint("/openapi/v1.json", "API v1");
  });
}

app.UseStaticFiles(
  new StaticFileOptions
  {
    FileProvider = new PhysicalFileProvider(
      Path.Combine(builder.Environment.ContentRootPath, "Uploads")
    ),
    RequestPath = "/images",
  }
);

app.UseAuthorization();
app.MapControllers();

app.Run();
