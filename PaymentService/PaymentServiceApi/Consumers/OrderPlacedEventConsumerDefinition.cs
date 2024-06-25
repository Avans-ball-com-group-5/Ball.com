using MassTransit;

namespace PaymentServiceApi.Consumers
{
    public class OrderPlacedEventConsumerDefinition : ConsumerDefinition<OrderPlacedEventConsumer>
    {
        public OrderPlacedEventConsumerDefinition()
        {
            EndpointName = "order-placed";
        }
    }
}