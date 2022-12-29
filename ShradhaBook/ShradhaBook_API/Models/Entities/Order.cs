using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShradhaBook_API.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        [JsonIgnore]
        public User User { get; set; } = null!;
        public int UserId { get; set; }
        public string PaymentForms { get; set; } = "CashOnDelivery";
        public bool Payment { get; set; } = false;
        public string OrderTracking { get; set; } = "Preparing";
        public DateTime TrackingUpdateTime { get; set; }
        [Column(TypeName = "decimal(16,4)")]
        public decimal Total { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
        public bool Cancellation { get; set; } = false;
    }
}
