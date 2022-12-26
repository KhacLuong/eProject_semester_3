using Newtonsoft.Json;

namespace ShradhaBook_API.Models.Entities
{
    public class Order
    {
        public enum PaymentForm
        {
            CashOnDelivery,
            OnlinePayment
        }
        public enum OrderStatus
        {
            Preparing,
            Shipping,
            Delivered
        }
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        [JsonIgnore]
        public User User { get; set; } = null!;
        public int UserId { get; set; }
        public PaymentForm PaymentForms { get; set; } = PaymentForm.CashOnDelivery;
        public bool Payment { get; set; } = false;
        public OrderStatus OrderTracking { get; set; } = OrderStatus.Preparing;
        public DateTime TrackingUpdateTime { get; set; }
        public decimal Total { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>();

        public Order()
        {
            OrderNumber = Id.ToString("D8");
        }
    }
}
