using MassTransit;
using PaymentDomain.Events.Input;
using PaymentService.Handlers;

namespace PaymentService.Consumers
{
    public class OrderPlacedEventConsumer : IConsumer<OrderPlacedEvent>
    {
        private readonly PaymentHandler paymentHandler;

        public OrderPlacedEventConsumer(PaymentHandler paymentHandler)
        {
            this.paymentHandler = paymentHandler;
        }

        public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
        {
            await paymentHandler.HandleOrderPlacedEvent(context.Message);
        }
    }
}