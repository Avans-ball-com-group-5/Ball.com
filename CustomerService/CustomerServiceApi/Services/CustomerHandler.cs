using Domain.Events;
using MassTransit;

namespace CustomerServiceApi.Services
{
    public class CustomerHandler
    {
        private readonly IBus Bus;
        public CustomerHandler(IBus bus)
        {
            Bus = bus;
        }

        public async Task RegisterCustomerServiceTicket(RegisterCustomerServiceTicket registerEvent)
        {
            // Do something with the ticket info, like saving it to a database or sending it to another service
            // For now, we'll just publish the created event
            Console.WriteLine("Registering customer service...");

            await Bus.Publish(
                new CustomerServiceTicketRegistered(
                    registerEvent.CustomerId,
                    registerEvent.Message,
                    Guid.NewGuid()));
        }
    }
}
