using Books.Infrastructure.Data;
using Books.Models;
using Books.Models.Mappers;
using Books.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Books.Utils.Profiles;
using Books.Services.Interface;
using Books.Services.Repository;
using Books.Models.Dtos;
using Books.Models.DTOs;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetEnv;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Env.Load();

builder.Services.AddDbContext<BaseContext>(options => options.UseMySql(
                        builder.Configuration.GetConnectionString("DbConnection"),
                        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));


//JWT
builder.Services.AddAuthentication(opt => {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(configure => {
        configure.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = @Environment.GetEnvironmentVariable("JwtToken"),
            ValidAudience = @Environment.GetEnvironmentVariable("JwtToken"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(@Environment.GetEnvironmentVariable("SecretKey")))
        };
    });

// Configuration of the Interface that we will be used
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailRepository, EmailRepository>();
builder.Services.AddTransient<IExcelExportRepository, ExcelExportRepository>();



// Register AutoMapper profiles
builder.Services.AddAutoMapper(typeof(UsersProfile));

// Configuration of controllers
builder.Services.AddControllers();


builder.Services.AddTransient<IValidator<UserDto>, UserDtoValidator>();
builder.Services.AddTransient<IValidator<BookDTO>, BookDtoValidator>();
builder.Services.AddTransient<IValidator<AuthorDTO>, AutorDtoValidator>();


builder.Services.AddAutoMapper(typeof(BooksProfile));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();    

app.Run();