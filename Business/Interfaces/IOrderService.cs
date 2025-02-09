using OrderManagementSystem.Data.Enums;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Business.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<Order?> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
    }
}
