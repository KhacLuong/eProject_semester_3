namespace ShradhaBook_API.Services.OrderItemsService
{
    public interface IOrderItemsService
    {
        public Task<OrderItems?> CreateOrderItems(CreateOrderItemsRequest request);
    }
}
