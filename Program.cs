using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi;
using NoxVendor.WebApi.Extensions;
using NoxVendor.WebApi.Middlewares;
using NoxVendor.WebApi.Services;
using Scalar.AspNetCore;

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
builder.Services.AddLogging();

builder.Services.AddOpenApi(options =>
{
  options.AddDocumentTransformer(
    (document, context, cancellationToken) =>
    {
      document.Components ??= new OpenApiComponents();
      document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>
      {
        ["Bearer"] = new OpenApiSecurityScheme
        {
          Type = SecuritySchemeType.Http,
          Scheme = "bearer",
          In = ParameterLocation.Header,
          BearerFormat = "JWT",
        },
      };
      foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations!))
      {
        operation.Value.Security ??= [];

        operation.Value.Security.Add(
          new OpenApiSecurityRequirement
          {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = [],
          }
        );
      }

      document.SetReferenceHostDocument();

      return Task.CompletedTask;
    }
  );
});

// Extensions
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentityServices();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddSingleton<TokenService>();

var app = builder.Build();

app.UseRouting();

app.UseCors("AllowFrontendApp");

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();

  app.UseSwaggerUI(options =>
  {
    options.SwaggerEndpoint("/openapi/v1.json", "NoxVendor API v1");
  });

  app.MapScalarApiReference(options =>
  {
    options
      .WithTitle("NoxVendor API")
      .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
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

app.UseMiddleware<ExceptionHandler>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
