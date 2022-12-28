namespace ShradhaBook_API.Services.OrderService
{
    public interface IOrderService
    {
        public Task<Order?> CreateOrder(CreateOrderRequest request);
    }
}
