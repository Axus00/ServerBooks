using Books.Infrastructure.Data;
using Books.Models;
using Books.Models.Mappers;
using Books.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Books.Models;
using AutoMapper;
using Books.Utils.Profiles;
using Books.Services.Interface;
using Books.Services.Repository;
using MailKit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BaseContext>(options => options.UseMySql(
                        builder.Configuration.GetConnectionString("DbConnection"),
                        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));

// Configuration of the Interface that we will be used
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailRepository, EmailRepository>();



// Register AutoMapper profiles
builder.Services.AddAutoMapper(typeof(UsersProfile));

// Configuration of controllers
builder.Services.AddControllers();


builder.Services.AddTransient<IValidator<User>, UserValidator>();
builder.Services.AddTransient<IValidator<UserData>, UserDataValidator>();
builder.Services.AddTransient<IValidator<Book>, BookValidator>();
builder.Services.AddTransient<IValidator<Autor>, AutorValidator>();

builder.Services.AddScoped<IBooksRepository, BooksRepository>();

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