using static ShradhaBook_API.Models.Entities.Order;

namespace ShradhaBook_API.Models.Request
{
    public class CreateOrderRequest
    {
        public int UserId { get; set; }
        public PaymentForm PaymentForms { get; set; } = PaymentForm.CashOnDelivery;
        public bool Payment { get; set; } = false;
        public decimal Total { get; set; }
    }
}
