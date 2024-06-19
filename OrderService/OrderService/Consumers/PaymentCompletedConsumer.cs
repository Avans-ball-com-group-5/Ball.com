using MassTransit;
using OrderDomain.Events;
using OrderService.Handlers;

namespace OrderService.Consumers
{
    public class PaymentCompletedConsumer : IConsumer<PaymentCompletedEvent>
    {
        private readonly OrderHandler _orderPublisher;
        public PaymentCompletedConsumer(OrderHandler orderPublisher)
        {
            _orderPublisher = orderPublisher;
        }

        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            Console.WriteLine("Starting order management...");
            await _orderPublisher.ManageOrder(context.Message);
        }
    }
}
