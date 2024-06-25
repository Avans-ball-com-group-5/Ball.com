using Domain.Events;
using MassTransit;
using PaymentServiceApi.Handlers;

namespace PaymentServiceApi.Consumers
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