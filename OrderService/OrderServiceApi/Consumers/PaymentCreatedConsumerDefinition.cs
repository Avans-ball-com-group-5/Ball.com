using MassTransit;

namespace OrderServiceApi.Consumers
{
    public class PaymentCreatedConsumerDefinition : ConsumerDefinition<PaymentCreatedConsumer>
    {
        public PaymentCreatedConsumerDefinition()
        {
            // The endpoint name is the name of the service that will be accepting the message
            EndpointName = "payment-completed";
        }
    }
}
