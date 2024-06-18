using MassTransit;
using OrderDomain.Events;
using OrderService.Services;

namespace OrderService.Messaging
{
    public class PaymentCompletedConsumer : IConsumer<PaymentCompletedEvent>
    {
        private readonly OrderPublisher _orderPublisher;
        public PaymentCompletedConsumer(OrderPublisher orderPublisher)
        {
            _orderPublisher = orderPublisher;
        }

        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            Console.WriteLine("Starting order management...");
            await _orderPublisher.ManageOrder(context.Message.Order);
        }
    }
}
