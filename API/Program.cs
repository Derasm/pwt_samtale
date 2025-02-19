using System.Text;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register services.
builder.Services.AddScoped<IVareService, VareService>();
builder.Services.AddScoped<IBeholdningService, BeholdningService>();
builder.Services.AddScoped<IVareDTOService, VareDTOService>();
// Add EF Core DbContext
builder.Services.AddDbContextFactory<PwtDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PWT_Test_DB")));
builder.Services.AddControllers();

//JWT Authenticaton

#region JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = jwtIssuer,
        ValidIssuer = jwtIssuer,
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true,
    };
});


#endregion

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

