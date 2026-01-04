using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FerreteríaWeb_Backend.DAOs;
using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Data;
using FerreteríaWeb_Backend.Services;
using FerreteríaWeb_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FerreteriaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

builder.Services.AddScoped<IEmployeeDao, EmployeeDao>();
builder.Services.AddScoped<IProviderDao, ProviderDao>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICategoryDao, CategoryDao>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProviderService, ProviderService>();
builder.Services.AddScoped<IProductDao, ProductDao>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


//app.UseHttpsRedirection();
app.UseCors("AllowVueApp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
