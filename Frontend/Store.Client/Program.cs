using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7114") });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7114/") });
Console.WriteLine("Starting web host");

await builder.Build().RunAsync();