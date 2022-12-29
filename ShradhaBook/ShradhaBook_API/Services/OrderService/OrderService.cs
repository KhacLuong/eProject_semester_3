using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace ShradhaBook_API.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly DataContext _context;

    public OrderService(DataContext context)
    {
        _context = context;
    }

    public async Task<Order?> CreateOrder(CreateOrderRequest request)
    {
        var user = await _context.Users.FindAsync(request.UserId);
        if (user == null) return null;

        var id = _context.Orders.OrderBy(o => o.Id).Last().Id;
        var order = new Order
        {
            OrderNumber = (id + 1).ToString("D8"),
            UserId = user.Id,
            PaymentForms = request.PaymentForms,
            OrderTracking = "Prepare",
            Total = request.Total
        };
        if (request.PaymentForms == "CashOnDelivery")
            order.Payment = false;
        else if (request.PaymentForms == "OnlinePayment") order.Payment = true;
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<List<Order>?> GetAllOrders(string? orderNumber,
        int? productId, int? userId, string? orderTracking,
        string? paymentForms, decimal? fromTotal, decimal? toTotal)
    {
        var orders = await _context.Orders.Where(o =>
                (string.IsNullOrEmpty(orderNumber) || orderNumber.All(char.IsDigit) ||
                 o.OrderNumber.Contains(orderNumber))
                && (userId == null || o.UserId == userId)
                && (string.IsNullOrEmpty(orderTracking) || o.OrderTracking.ToLower() == orderTracking.ToLower())
                && (string.IsNullOrEmpty(paymentForms) || o.PaymentForms.ToLower() == paymentForms.ToLower())
                && (fromTotal == null || o.Total > fromTotal)
                && (toTotal == null || o.Total < toTotal)
                && o.OrderItems.Any(oi => productId == null || oi.ProductId == productId))
            .IncludeFilter(o => o.OrderItems.Where(oi => productId == null || oi.ProductId == productId))
            .ToListAsync();
        return orders;
    }

    public async Task<List<Order>?> GetAllOrdersByUserId(int userId)
    {
        var orders = await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
        return orders;
    }

    public async Task<Order?> GetOrderById(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return null;
        return order;
    }

    public async Task<Order?> UpdateOrder(int id, UpdateOrderRequest request)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return null;

        order.Payment = request.Payment;
        order.OrderTracking = request.OrderTracking;
        order.TrackingUpdateTime = DateTime.Now;
        order.UpdateAt = DateTime.Now;

        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<string?> CancelOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return null;
        order.Cancellation = true;

        await _context.SaveChangesAsync();
        return "order cancelled";
    }
}