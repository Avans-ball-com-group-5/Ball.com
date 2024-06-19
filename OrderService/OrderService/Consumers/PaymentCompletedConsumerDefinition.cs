using MassTransit;

namespace OrderService.Consumers
{
    public class PaymentCompletedConsumerDefinition : ConsumerDefinition<PaymentCompletedConsumer>
    {
        public PaymentCompletedConsumerDefinition()
        {
            // The endpoint name is the name of the service that will be accepting the message
            EndpointName = "payment-completed";
        }
    }
}
