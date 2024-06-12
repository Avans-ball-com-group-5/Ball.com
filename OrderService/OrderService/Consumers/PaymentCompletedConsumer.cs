using MassTransit;
using OrderDomain.Events;
using OrderService.Services;

namespace OrderService.Messaging
{
    public class RegisterCustomerServiceConsumer : IConsumer<RegisterCustomerService>
    {
        private readonly OrderHandler _customerHandler;
        public RegisterCustomerServiceConsumer(OrderHandler customerHandler)
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
