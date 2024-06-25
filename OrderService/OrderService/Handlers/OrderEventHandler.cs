using Domain;
using Domain.Events;
using Domain.Services;
using MassTransit;

namespace OrderService.Handlers
{
    public class OrderEventHandler
    {
        private readonly IBus _bus;
        private readonly IOrderCommandHandler _orderCommandHandler;
        private readonly IOrderQueryHandler _orderQueryHandler;
        public OrderEventHandler(IBus bus, IOrderCommandHandler orderCommandHandler, IOrderQueryHandler orderQueryHandler)
        {
            _bus = bus;
            _orderCommandHandler = orderCommandHandler;
            _orderQueryHandler = orderQueryHandler;
        }

        public async Task PlaceOrder(PlaceOrderEvent @event)
        {
            _orderCommandHandler.SaveOrderEvent(@event);

            // Order was placed, store event and publish to event bus
            var orderPlacedEvent = new OrderPlacedEvent(@event.OrderId)
            {
                Timestamp = DateTime.UtcNow,
            };
            _orderCommandHandler.SaveOrderEvent(orderPlacedEvent);
            await _bus.Publish(orderPlacedEvent);
        }
        
        public async Task ManageOrder(PaymentCreatedEvent @event)
        {
            Console.WriteLine($"Payment created for order {@event.OrderId}");
            _orderCommandHandler.SaveOrderEvent(@event);

            var orderPackagedEvent = new OrderPackagedEvent(@event.OrderId);
            _orderCommandHandler.SaveOrderEvent(orderPackagedEvent);

            var order = _orderQueryHandler.GetAggregateById(orderPackagedEvent.OrderId);
            Console.WriteLine($"Order: {order}");

            await _bus.Publish(
                new OrderReadyForShippingEvent(@event.OrderId)
                {
                    Timestamp = DateTime.UtcNow,
                    Order = order
                });
        }
    }
}
