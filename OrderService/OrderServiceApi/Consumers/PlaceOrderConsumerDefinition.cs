using MassTransit;

namespace OrderServiceApi.Consumers
{
    public class PlaceOrderConsumerDefinition : ConsumerDefinition<PlaceOrderConsumer>
    {
        public PlaceOrderConsumerDefinition()
        {
            // The endpoint name is the name of the service that will be accepting the message
            EndpointName = "place-order";
        }
    }
}
