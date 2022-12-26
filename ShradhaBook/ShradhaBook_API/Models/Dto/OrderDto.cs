using static ShradhaBook_API.Models.Entities.Order;

namespace ShradhaBook_API.Models.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
        public decimal Total { get; set; }
        public PaymentForm PaymentForms { get; set; }
        public List<Transaction>? Transactions { get; set; }
        public OrderStatus OrderTracking { get; set; }
        public DateTime TrackingUpdateTime { get; set; }
    }
}
