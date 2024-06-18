using MassTransit;
using PaymentDomain.Events.Input;
using PaymentDomain.Events.Output;

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
    }
}