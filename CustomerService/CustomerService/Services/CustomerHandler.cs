using CustomerDomain.Events.Input;
using CustomerDomain.Events.Output;
using MassTransit;

namespace CustomerService.Services
{
    public class CustomerHandler
    {
        private readonly IBus Bus;
        public CustomerHandler(IBus bus)
        {
            Bus = bus;
        }

        public async Task RegisterCustomerService(RegisterCustomerService registerEvent)
        {
            // Do something with the ticket info, like saving it to a database or sending it to another service
            // For now, we'll just publish the created event
            Console.WriteLine("Registering customer service...");

            await Bus.Publish(
                new CustomerServiceRegistered(
                    registerEvent.CustomerId,
                    registerEvent.Name,
                    registerEvent.Email,
                    registerEvent.Phone,
                    registerEvent.Message,
                    Guid.NewGuid()));
        }
    }
}
