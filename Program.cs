using Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Store.ApplicationCore.Mappings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BaseContext>(options => options.UseMySql(
                        builder.Configuration.GetConnectionString("DbConnection"),
                        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));

// Automapers
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

app.Run();