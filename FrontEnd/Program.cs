using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using frontend;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5200") });

//Copilot helped in suggesting a timeout for the HttpClient to avoid long waits in case of network issues
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:5200"),
    Timeout = TimeSpan.FromSeconds(30)
});

// Register ProductService
builder.Services.AddScoped<ProductService>();

await builder.Build().RunAsync();