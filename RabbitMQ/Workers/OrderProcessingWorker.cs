using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using OrderManagementSystem.Models;
using OrderManagementSystem.Data.Enums;
using OrderManagementSystem.Business.Interfaces;

namespace OrderManagementSystem.RabbitMQ.Workers
{
    public class OrderProcessingWorker : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IOrderService _orderService;

        public OrderProcessingWorker(IOrderService orderService)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "orders", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _orderService = orderService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var order = System.Text.Json.JsonSerializer.Deserialize<Order>(message);
                await _orderService.UpdateOrderStatusAsync(order.Id, OrderStatus.Processed);
            };

            _channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
