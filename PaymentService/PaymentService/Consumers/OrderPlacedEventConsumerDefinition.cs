using MassTransit;

namespace PaymentService.Consumers
{
    public class OrderPlacedEventConsumerDefinition : ConsumerDefinition<OrderPlacedEventConsumer>
    {
        public OrderPlacedEventConsumerDefinition()
        {
            EndpointName = "order-placed-event";
        }
    }
}
