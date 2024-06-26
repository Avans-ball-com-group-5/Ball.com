using MassTransit;

namespace OrderServiceApi.Consumers
{
    public class OrderDenormalizerConsumerDefinition : ConsumerDefinition<OrderDenormalizerConsumer>
    {
        public OrderDenormalizerConsumerDefinition()
        {
            // The endpoint name is the name of the service that will be accepting the message
            EndpointName = "order-denormalize";
        }
    }
}
