using OrderManagementSystem.Models;
using OrderManagementSystem.RabbitMQ.Services;
using OrderManagementSystem.Repository.Interfaces;
using OrderManagementSystem.Business.Interfaces;
using OrderManagementSystem.Data.Enums;

namespace OrderManagementSystem.Business.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly RabbitMqService _rabbitMqService;

        public OrderService(IOrderRepository orderRepository, RabbitMqService rabbitMqService)
        {
            _orderRepository = orderRepository;
            _rabbitMqService = rabbitMqService;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
            _rabbitMqService.SendMessage(System.Text.Json.JsonSerializer.Serialize(order));
            return order;
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return null;
            }

            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order != null)
            {
                order.Status = Convert.ToInt32(status);
                await _orderRepository.UpdateOrderAsync(order);
            }
        }
    }
}
