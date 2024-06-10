using MassTransit;

namespace CustomerService.Messaging
{
    public class RegisterCustomerServiceConsumerDefinition : ConsumerDefinition<RegisterCustomerServiceConsumer>
    {
        public RegisterCustomerServiceConsumerDefinition() 
        {
            // The endpoint name is the name of the service that will be accepting the message
            EndpointName = "register-customer-service";
        }
    }
}
