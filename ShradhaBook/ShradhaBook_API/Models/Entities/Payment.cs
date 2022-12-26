namespace ShradhaBook_API.Models.Entities
{
    public class Payment
    {
        public enum PaymentMethod
        {
            Deposit,
            CashOnDelivery,
            OnlinePayment
        }
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public decimal Tax { get; set; }
        public string Currency { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public PaymentMethod Method { get; set; }
        public Order Order { get; set; } = null!;
        public int OrderId { get; set; }
        public User User { get; set; } = null!;
        public int UserId { get; set; }
        public decimal FinalTotal { get; set; }

    }
}
