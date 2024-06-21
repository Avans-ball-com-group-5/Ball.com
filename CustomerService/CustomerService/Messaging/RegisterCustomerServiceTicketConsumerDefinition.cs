using MassTransit;

namespace CustomerService.Messaging
{
    public class RegisterCustomerServiceTicketConsumerDefinition : ConsumerDefinition<RegisterCustomerServiceTicketConsumer>
    {
        public RegisterCustomerServiceTicketConsumerDefinition() 
        {
            // The endpoint name is the name of the service that will be accepting the message
            EndpointName = "register-customer-service-ticket";
        }
    }
}
