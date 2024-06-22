using MassTransit;
using ProductDomain.Events.Input;
using ProductDomain.Events.Output;

namespace ProductService.Services
{
    public class ProductHandler
    {
        private readonly IBus Bus;
        public ProductHandler(IBus bus)
        {
            Bus = bus;
        }

        public async Task RegisterProductService(RegisterProductService registerEvent)
        {
            // Do something with the ticket info, like saving it to a database or sending it to another service
            // For now, we'll just publish the created event
            Console.WriteLine("Registering Product service...");

            // Save the product to the database
            // Send a message to the customer service to notify them of the new product
            await Bus.Publish(
                new ProductServiceRegistered(
                    registerEvent.Id,
                    registerEvent.Name,
                    registerEvent.Description,
                    registerEvent.Company,
                    registerEvent.Price,
                    registerEvent.Stock
                    ));
        }
    }
}
