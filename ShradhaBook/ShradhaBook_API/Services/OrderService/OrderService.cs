namespace ShradhaBook_API.Services.OrderService
{
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
            if (user == null)
            {
                return null;
            }

            var order = new Order
            {
                UserId = request.UserId,
                PaymentForms = request.PaymentForms,
                Total = request.Total
            };
            if (request.PaymentForms == Order.PaymentForm.CashOnDelivery)
            {
                order.Payment = false;
            }
            else if (request.PaymentForms == Order.PaymentForm.OnlinePayment)
            {
                order.Payment = true;
            }
            _context.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }
    }
}
