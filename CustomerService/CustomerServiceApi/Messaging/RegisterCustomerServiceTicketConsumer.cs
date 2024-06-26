using CustomerServiceApi.Services;
using Domain.Events;
using MassTransit;

namespace CustomerServiceApi.Messaging
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
            await _customerHandler.RegisterCustomerServiceTicket(context.Message);
        }
    }
}
