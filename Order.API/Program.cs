using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repositories;
using Order.Application.Services;
using Order.Domain.Interfaces;
using Order.Application.Clients;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositorio
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Servicio
builder.Services.AddScoped<OrderService>();

// DbContext
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<ProductClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7154");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
