namespace ShradhaBook_API.Services.OrderItemsService;

public class OrderItemsService : IOrderItemsService
{
    private readonly DataContext _context;

    public OrderItemsService(DataContext context)
    {
        _context = context;
    }

    public async Task<OrderItems?> CreateOrderItems(CreateOrderItemsRequest request)
    {
        var product = await _context.Products.FindAsync(request.ProductId);
        if (product == null) return null;
        product.Quantity -= request.Quantity;
        if (product.Quantity < 0)
            return null;
        var lastOrder = _context.Orders.OrderByDescending(oi => oi.Id).Last();
        var orderItems = new OrderItems
        {
            OrderId = lastOrder.Id,
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice,
            Subtotal = request.Quantity * request.UnitPrice
        };
        _context.OrderItems.Add(orderItems);
        await _context.SaveChangesAsync();

        return orderItems;
    }
}