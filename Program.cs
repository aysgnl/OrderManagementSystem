using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.RabbitMQ.Services;
using OrderManagementSystem.RabbitMQ.Workers;
using OrderManagementSystem.Repository.Implementations;
using OrderManagementSystem.Repository.Interfaces;
using OrderManagementSystem.Business.Implementations;
using OrderManagementSystem.Business.Interfaces;
using OrderManagementSystem.Options;

var builder = WebApplication.CreateBuilder(args);

// Yapılandırma ayarlarını kaydet
builder.Services.Configure<RabbitMQOptions>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("Database"));

// Veritabanı bağlamını yapılandır
var databaseOptions = builder.Configuration.GetSection("Database").Get<DatabaseOptions>();
if (databaseOptions.UseInMemoryDatabase)
{
    builder.Services.AddDbContext<OrderContext>(opt => opt.UseInMemoryDatabase("OrderDb"));
}
else
{
    builder.Services.AddDbContext<OrderContext>(opt => opt.UseSqlite(databaseOptions.ConnectionString));
}

// Repository ve Service'leri kaydet
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

// RabbitMQ servisini kaydet
builder.Services.AddSingleton<RabbitMqService>();

// Worker Service'i kaydet
builder.Services.AddHostedService<OrderProcessingWorker>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
