using Microsoft.Extensions.Options;
using OrderManagementSystem.Options;
using RabbitMQ.Client;
using System.Text;

namespace OrderManagementSystem.RabbitMQ.Services
{
    public class RabbitMqService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;

        public RabbitMqService(IOptions<RabbitMQOptions> options)
        {
            var rabbitMQOptions = options.Value;
            var hostName = rabbitMQOptions.HostName;
            _queueName = rabbitMQOptions.QueueName;

            var factory = new ConnectionFactory() { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
