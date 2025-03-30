using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy to the service collection
//Copilot helped in this section to add CORS policy to the service collection since my front end and backend were running on different ports
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddMemoryCache(); // Register memory cache
var app = builder.Build();

app.UseCors("AllowAll"); // Use the CORS policy

// Apply the CORS policy before mapping routes
app.MapGet("/api/productlist", (IMemoryCache cache) =>
{
    return cache.GetOrCreate("productListCache", entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5); // Cache for 5 minutes
        return new[]
        {
            new
            {
                Id = 1,
                Name = "Laptop",
                Price = 1200.50,
                Stock = 25,
                Category = new { Id = 101, Name = "Electronics" }
            },
            new
            {
                Id = 2,
                Name = "Headphones",
                Price = 50.00,
                Stock = 100,
                Category = new { Id = 102, Name = "Accessories" }
            }
        };
    });
});

app.Run();
