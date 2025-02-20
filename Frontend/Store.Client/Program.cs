using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Store.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7114/") 
    
});
Console.WriteLine("Starting web host");

builder.Services.AddScoped<IProductService, ProductService>();
await builder.Build().RunAsync();