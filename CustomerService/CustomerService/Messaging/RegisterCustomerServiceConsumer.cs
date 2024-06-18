using CustomerService.Services;
using CustomerDomain.Events.Input;
using MassTransit;

namespace CustomerService.Messaging
{
    public class RegisterCustomerServiceConsumer : IConsumer<RegisterCustomerService>
    {
        private readonly CustomerHandler _customerHandler;
        public RegisterCustomerServiceConsumer(CustomerHandler customerHandler)
        {
            _customerHandler = customerHandler;
        }

        public async Task Consume(ConsumeContext<RegisterCustomerService> context)
        {
            await _customerHandler.RegisterCustomerService(context.Message);
        }
    }
}
