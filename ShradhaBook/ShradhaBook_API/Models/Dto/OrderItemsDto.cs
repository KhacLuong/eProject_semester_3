namespace ShradhaBook_API.Models.Dto;

public class OrderItemsDto
{
    public int Id { get; set; }
    public Product Products { get; set; } = new();
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
}