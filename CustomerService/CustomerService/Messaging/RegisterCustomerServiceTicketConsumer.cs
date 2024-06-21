using CustomerService.Services;
using CustomerDomain.Events.Input;
using MassTransit;

namespace CustomerService.Messaging
{
    public class RegisterCustomerServiceTicketConsumer : IConsumer<RegisterCustomerServiceTicket>
    {
        private readonly CustomerHandler _customerHandler;
        public RegisterCustomerServiceTicketConsumer(CustomerHandler customerHandler)
        {
            _customerHandler = customerHandler;
        }

        public async Task Consume(ConsumeContext<RegisterCustomerServiceTicket> context)
        {
            await _customerHandler.RegisterCustomerService(context.Message);
        }
    }
}
