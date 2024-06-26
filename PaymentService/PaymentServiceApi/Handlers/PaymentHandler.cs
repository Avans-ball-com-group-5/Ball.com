using Domain;
using Domain.Events;
using Domain.Services;
using MassTransit;

namespace PaymentServiceApi.Handlers
{
    public class PaymentHandler
    {
        private readonly IBus _bus;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentHandler(IBus bus, IPaymentRepository paymentRepository)
        {
            _bus = bus;
            _paymentRepository = paymentRepository;
        }

        public async Task HandleOrderPlacedEvent(OrderPlacedEvent request)
        {
            var isCompleted = false;
            if (new Random().Next(0, 2) == 0)
            {
                isCompleted = true;
            }
            var paymentCreatedEvent = new PaymentCreatedEvent()
            {
                OrderId = request.OrderId,
                IsCompleted = isCompleted
            };

            Payment payment = new()
            {
                Amount = new Random().Next(0, 100),
                IsAfterPay = !isCompleted,
                OrderId = request.OrderId,
                IsPaid = isCompleted
            };

            _paymentRepository.AddPayment(payment);
            await _bus.Publish(paymentCreatedEvent);
        }

        public async Task HandleAfterPayCompletedEvent(Guid message)
        {
            Payment payment = _paymentRepository.GetPaymentById(message);

            if (!payment.IsPaid)
            {
                var orderPaymentCompletedEvent = new OrderPaymentCompletedEvent()
                {
                    OrderId = Guid.NewGuid(),
                    PaymentId = message,
                };

                payment.IsPaid = true;
                _paymentRepository.UpdatePayment(payment);
                await _bus.Publish(orderPaymentCompletedEvent);
            }
            else
            {
                Console.WriteLine("Payment is already completed!");
            }
        }
    }
}