using Domain.Events;
using MassTransit;
using OrderService.Handlers;

namespace OrderService.Consumers
{
    public class PaymentCreatedConsumer : IConsumer<PaymentCreatedEvent>
    {
        private readonly OrderHandler _orderPublisher;
        public PaymentCreatedConsumer(OrderHandler orderPublisher)
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
