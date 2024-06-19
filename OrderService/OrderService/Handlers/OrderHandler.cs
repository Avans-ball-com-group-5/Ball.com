using MassTransit;
using OrderDomain;
using OrderDomain.Events;
using OrderDomain.Services;

namespace OrderService.Handlers
{
    public class OrderHandler
    {
        private readonly IBus _bus;
        private readonly IOrderRepository _orderRepository;
        public OrderHandler(IBus bus, IOrderRepository orderRepository)
        {
            _bus = bus;
            _orderRepository = orderRepository;
        }

        public async Task PlaceOrder(Order order)
        {
            // Order was placed, store event and publish to event bus
            var orderPlacedEvent = new OrderPlacedEvent()
            {
                OrderId = order.Id,
                Timestamp = DateTime.UtcNow,
            };
            _orderRepository.SaveOrderEvent(orderPlacedEvent);
            await _bus.Publish(orderPlacedEvent);
        }

        public async Task ManageOrder(PaymentCompletedEvent paymentCompletedEvent)
        {
            // Item picking & Order packaging
            // TODO: Remove dummy data
            List<ItemRef> itemRefs = new()
            {
                new()
                {
                    Amount = 1
                },
                new()
                {
                    Amount = 1
                }
            };
            var orderPackagedEvent = new OrderPackagedEvent()
            {
                Items = itemRefs
            };
            _orderRepository.SaveOrderEvent(orderPackagedEvent);

            // Logistics selection

            await _bus.Publish(
                new OrderReadyForShippingEvent()
                {
                    OrderId = paymentCompletedEvent.OrderId,
                });
        }
    }
}
