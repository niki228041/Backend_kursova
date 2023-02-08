using _3dd_Data;
using _3dd_Data.Db;
using _3dd_Data.DbSeeder;
using _3dd_Data.Models;
using _3dd_Data.Repositories;
using _3dd_Data.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Unit_Data.AutoMapper;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var googleAuthSettings = builder.Configuration
    .GetSection("GoogleAuthSettings")
    .Get<GoogleAuthSettings>();

builder.Services.AddSingleton(googleAuthSettings);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContext3d>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ICompanyRepository,CompanyRepository>(); 
builder.Services.AddScoped<IProductCommentRepository, ProductCommentRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();


//builder.Services.AddTransient<ProductRepository>();

builder.Services.AddIdentity<MyAppUser, MyAppRole>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 5;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<DbContext3d>().AddDefaultTokenProviders();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options
    .WithOrigins("http://localhost:3000", "http://localhost:3001", "http://194.44.93.225", "http://10.7.101.243", "http://52.188.227.148", "http://40.76.116.183", "http://192.168.0.104")
    .AllowAnyHeader()
    .AllowCredentials()
    .AllowAnyMethod()
    );

app.UseHttpsRedirection();

app.UseAuthorization();

var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
if(!Directory.Exists(dir))
{
    Directory.CreateDirectory(dir);
}


app.UseStaticFiles(
    new StaticFileOptions {
    FileProvider = new PhysicalFileProvider(dir),
    RequestPath = "/images"
    });


app.MapControllers();

app.SeedData();

app.Run();
