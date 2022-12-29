namespace ShradhaBook_API.Services.OrderService
{
    public interface IOrderService
    {
        public Task<Order?> CreateOrder(CreateOrderRequest request);
        public Task<List<Order>?> GetAllOrders(string? orderNumber, 
            int? productId, int? userId, string? orderTracking, 
            string? paymentForms, decimal? fromTotal, decimal? toTotal);
        public Task<List<Order>?> GetAllOrdersByUserId(int userId);
        public Task<Order?> GetOrderById(int id);
        public Task<Order?> UpdateOrder(int id, UpdateOrderRequest request);
        public Task<string?> CancelOrder(int id);
    }
}
