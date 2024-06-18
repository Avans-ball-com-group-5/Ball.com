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

        public async Task PlaceOrder(Order order)
        {
            // Do something with the order like saving to database, and publish created event
            Console.WriteLine("Creating order");

            await Bus.Publish(
                new OrderPlacedEvent()
                {
                    OrderId = order.Id,
                    Timestamp = DateTime.UtcNow,
                });
        }

        public async Task ManageOrder(PaymentCompletedEvent paymentCompletedEvent) {
            // Item picking

            // Order packaging

            // Logistics selection

            await Bus.Publish(
                new OrderReadyForShippingEvent()
                {
                    OrderId = paymentCompletedEvent.OrderId,
                    Message = "order is ready for shipment"
                });
        }
    }
}
