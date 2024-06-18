using MassTransit;
using OrderDomain;
using OrderDomain.Events;

namespace OrderService.Services
{
    public class OrderPublisher
    {
        private readonly IBus Bus;
        public OrderPublisher(IBus bus)
        {
            Bus = bus;
        }

        public async Task PlaceOrder(Order order)
        {
            // Do something with the order like saving to database, and publish created event
            Console.WriteLine("Creating order");

            await Bus.Publish(
                new OrderPlacedEvent()
                {
                    Order = order,
                    Message = "order was placed, waiting for payment"
                });
        }

        public async Task ManageOrder(Order order) {
            // Item picking

            // Order packaging

            // Logistics selection

            await Bus.Publish(
                new OrderReadyForShippingEvent()
                {
                    Order = order,
                    Message = "order is ready for shipment"
                });
        }
    }
}
