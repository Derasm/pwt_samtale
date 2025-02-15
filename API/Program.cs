using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register Generic Repository & Unit of Work
builder.Services.AddScoped(typeof(IGenericRepositoryInterface<>), typeof(PWT_Test_Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add EF Core DbContext
builder.Services.AddDbContextFactory<PwtDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PWT_Test_DB")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PwtDbContext>();

    var items = dbContext.Varer.Take(5).ToList();

    foreach (var item in items)
    {
        Console.WriteLine($"StyleNo: {item.StyleNo}, Description: {item.ItemDescription}");
    }
}

app.Run();

