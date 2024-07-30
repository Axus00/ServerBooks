using Books.Infrastructure.Data;
using Books.Models;
using Books.Models.Mappers;
using Books.Validators;
using FluentValidation;
using FluentValidation.AspNetCore; // K
using Microsoft.EntityFrameworkCore;
using Books.Utils.Profiles;
using Books.Services.Interface;
using Books.Services.Repository;
using Books.Models.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetEnv;
using Books.Utils.PasswordHashing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
/*     .AddFluentValidation(cfg => 
    {
        cfg.RegisterValidatorsFromAssemblyContaining<UserDTOValidator>();
        cfg.RegisterValidatorsFromAssemblyContaining<AdminUserDTOValidator>();
    }); //K */


// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Env.Load();

builder.Services.AddDbContext<BaseContext>(options => options.UseMySql(
    builder.Configuration.GetConnectionString("DbConnection"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));

// JWT Authentication
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
//----- Repository injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IBooksBorrowRepository, BooksBorrowRepository>();

builder.Services.AddScoped<Bcrypt>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IJwtRepository, JwtRepository>(); //K


// Register AutoMapper profiles
builder.Services.AddAutoMapper(typeof(UsersProfile));
builder.Services.AddAutoMapper(typeof(BooksProfile));

// Register FluentValidation validators
builder.Services.AddTransient<IValidator<UserDTO>, UserDTOValidator>();
builder.Services.AddTransient<IValidator<AdminUserDTO>, AdminUserDTOValidator>();
builder.Services.AddTransient<IValidator<BookDTO>, BookDTOValidator>();
builder.Services.AddTransient<IValidator<AuthorDTO>, AutorDTOValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();    

app.Run();
