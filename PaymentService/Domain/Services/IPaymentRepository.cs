namespace Domain.Services
{
    public interface IPaymentRepository
    {
        public void AddPayment(Payment payment);

        public Payment GetPaymentById(Guid id);

        public void UpdatePayment(Payment payment);
    }
}