using MassTransit;
using OrderDomain;
using OrderDomain.Events;

namespace OrderService.Services
{
    public class OrderHandler
    {
        private readonly IBus Bus;
        public OrderHandler(IBus bus)
        {
            Bus = bus;
        }

        public async Task CreateOrder(Order order)
        {
            // Do something with the order like saving to database, and publish created event
            Console.WriteLine("Creating order");

            await Bus.Publish(
                new OrderCreated()
                {
                    Id = new Guid(),
                    Order = order
                });
        }
    }
}
