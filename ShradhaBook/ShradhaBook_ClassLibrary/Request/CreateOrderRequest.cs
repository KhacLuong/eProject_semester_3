namespace ShradhaBook_ClassLibrary.Request;

public class CreateOrderRequest
{
    public string OrderNumber { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public List<CreateOrderItemsRequest> OrderItems { get; set; } = new();
    public string PaymentForms { get; set; } = string.Empty;
    public bool Payment { get; set; } = false;
    public string OrderTracking { get; set; } = string.Empty;
    public decimal Total { get; set; }
}