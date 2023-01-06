using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShradhaBook_ClassLibrary.Entities;

public class OrderItems
{
    public int Id { get; set; }

    [JsonIgnore] public Order Order { get; set; } = null!;

    public int OrderId { get; set; }

    [JsonIgnore] public Product Product { get; set; } = null!;

    public int ProductId { get; set; }
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(16,4)")] public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(16,4)")] public decimal Subtotal { get; set; }
}