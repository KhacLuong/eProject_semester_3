using System.Text.Json.Serialization;

namespace ShradhaBook_API.Models.Entities
{
    public class OrderItems
    {
        public int Id { get; set; }
        [JsonIgnore]
        public Order Order { get; set; } = null!;
        public int OrderId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; } = null!;
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }
}
