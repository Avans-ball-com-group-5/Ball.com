using Domain.Events;
using MassTransit;
using OrderServiceApi.Handlers;

namespace OrderServiceApi.Consumers
{
    public class PaymentCreatedConsumer : IConsumer<PaymentCreatedEvent>
    {
        private readonly OrderEventHandler _orderPublisher;
        public PaymentCreatedConsumer(OrderEventHandler orderPublisher)
        {
            _orderPublisher = orderPublisher;
        }

        public async Task Consume(ConsumeContext<PaymentCreatedEvent> context)
        {
            Console.WriteLine("Starting order management...");
            await _orderPublisher.ManageOrder(context.Message);
        }
    }
}
