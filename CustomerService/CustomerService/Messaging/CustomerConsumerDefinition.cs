using MassTransit;

namespace CustomerService.Messaging
{
    public class CustomerConsumerDefinition : ConsumerDefinition<CustomerConsumer>
    {
        public CustomerConsumerDefinition() 
        {
            // The endpoint name is the name of the service that will be accepting the message
            EndpointName = "customer-service";
        }
    }
}
