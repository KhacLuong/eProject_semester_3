global using ShradhaBook_API.Services.UserService;
global using ShradhaBook_API.Services.UserInfoService;
global using ShradhaBook_API.Services.AddressService;
global using ShradhaBook_API.Services.AuthService;
global using ShradhaBook_API.Services.EmailService;
global using ShradhaBook_API.Services.CategotyService;
global using ShradhaBook_API.Services.ManufacturerService;
global using ShradhaBook_API.Services.ProductService;
global using ShradhaBook_API.Services.ProductTagService;
global using ShradhaBook_API.Services.TagService;
global using ShradhaBook_API.Services.AuthorService;
global using ShradhaBook_API.Data;
global using ShradhaBook_API.Models;
global using ShradhaBook_API.Models.Dto;
global using ShradhaBook_API.Models.Entities;
global using ShradhaBook_API.Models.Request;
global using ShradhaBook_API.Models.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserInfoService, UserInfoService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();

builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddScoped<IProductTagService, ProductTagService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();


builder.Services.AddHttpContextAccessor();

// Add button for adding token (login)
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// Automapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,

        };
    });
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder => {
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

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
