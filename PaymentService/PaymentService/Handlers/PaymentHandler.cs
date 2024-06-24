using Domain.Events;
using MassTransit;

namespace PaymentService.Handlers
{
    public class PaymentHandler
    {
        private readonly IBus bus;

        public PaymentHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async Task HandleOrderPlacedEvent(OrderPlacedEvent request)
        {
            // Add payment to database here

            // Send payment created event
            if (request.IsAfterPay)
            {
                var paymentCreatedEvent = new PaymentCreatedEvent()
                {
                    OrderId = request.OrderId,
                };

                await bus.Publish(paymentCreatedEvent);
            }
            else
            {
                var paymentCreatedEvent = new PaymentCreatedEvent()
                {
                    OrderId = request.OrderId,
                    IsCompleted = true
                };

                await bus.Publish(paymentCreatedEvent);
            }
        }

        public async Task HandleAfterPayCompletedEvent(PayAfterPayRequest message)
        {
            // Update db entity to completed
            // Get orderId from db entity

            // Send OrderPaymentCompletedEvent
            var orderPaymentCompletedEvent = new OrderPaymentCompletedEvent()
            {
                OrderId = Guid.NewGuid(),
                PaymentId = message.PaymentId
            };

            await bus.Publish(orderPaymentCompletedEvent);
        }
    }
}