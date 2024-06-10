using CustomerService.Services;
using Domain.Events;
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
            // Do something with the customer info, like saving it to a database or sending it to another service
            Console.WriteLine("Message recieved!");
            await _customerHandler.RegisterCustomerService(context.Message);
        }
    }
}
