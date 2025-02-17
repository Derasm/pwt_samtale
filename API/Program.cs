using API.Data;
using API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register services.
builder.Services.AddScoped<IVareService, VareService>();
// Add EF Core DbContext
builder.Services.AddDbContextFactory<PwtDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PWT_Test_DB")));
builder.Services.AddControllers();


var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

