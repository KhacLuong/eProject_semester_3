namespace ShradhaBook_API.Models.Dto;

public class OrderDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public List<OrderItems> OrderItems { get; set; } = new();
    public decimal Total { get; set; }
    public string PaymentForms { get; set; } = string.Empty;
    public string OrderTracking { get; set; } = string.Empty;
    public DateTime TrackingUpdateTime { get; set; }
}