using OrderManagementSystem.Data.Enums;

namespace OrderManagementSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; } = Convert.ToInt32(OrderStatus.NotOrdered);
    }
}
