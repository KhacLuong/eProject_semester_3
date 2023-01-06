global using ShradhaBook_API.Services.UserService;
global using ShradhaBook_API.Services.UserInfoService;
global using ShradhaBook_API.Services.AddressService;
global using ShradhaBook_API.Services.AuthService;
global using ShradhaBook_API.Services.EmailService;
global using ShradhaBook_API.Services.OrderService;
global using ShradhaBook_API.Services.OrderItemsService;
global using ShradhaBook_API.Services.StorageService;
global using ShradhaBook_API.Services.CategoryService;
global using ShradhaBook_API.Services.ManufacturerService;
global using ShradhaBook_API.Services.ProductService;
global using ShradhaBook_API.Services.ProductTagService;
global using ShradhaBook_API.Services.TagService;
global using ShradhaBook_API.Services.AuthorService;
global using ShradhaBook_API.Services.BlogService;
global using ShradhaBook_API.Services.BlogTagService;
global using ShradhaBook_API.Services.WishListService;

global using ShradhaBook_API.Services.RateService;
global using ShradhaBook_API.Data;
global using ShradhaBook_API.Helpers;
global using ShradhaBook_ClassLibrary.Dto;
global using ShradhaBook_ClassLibrary.Entities;
global using ShradhaBook_ClassLibrary.Request;
global using ShradhaBook_ClassLibrary.Response;
global using ShradhaBook_ClassLibrary.ViewModels;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using ShradhaBook_API.Services.RateService;
using ShradhaBook_API.Services.WishListProductService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserInfoService, UserInfoService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemsService, OrderItemsService>();
builder.Services.AddScoped<IStorageService, StorageService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IProductTagService, ProductTagService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IBlogTagService, BlogTagService>();
builder.Services.AddScoped<IWishListService, WishListService>();
builder.Services.AddScoped<IWishListProductService, WishListProductService>();
builder.Services.AddScoped<IRateService, RateService>();

builder.Services.AddAzureClients(options =>
{
    options.AddBlobServiceClient(builder.Configuration.GetSection("Storage:ConnectionString").Value);
});

builder.Services.AddHttpContextAccessor();

// Add button for adding token (login)
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// Automapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddCors(p =>
    p.AddPolicy("corsapp", builder => { builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();