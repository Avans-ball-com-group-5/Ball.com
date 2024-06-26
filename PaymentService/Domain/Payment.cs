namespace Domain
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public bool IsAfterPay { get; set; }
        public bool IsPaid { get; set; }
    }
}
