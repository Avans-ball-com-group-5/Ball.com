﻿using Domain;
using Domain.Events;
using Domain.Services;
using MassTransit;

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

        public async Task PlaceOrder(PlaceOrderEvent @event)
        {
            _orderRepository.SaveOrderEvent(@event);

            // Order was placed, store event and publish to event bus
            var orderPlacedEvent = new OrderPlacedEvent(@event.OrderId)
            {
                Timestamp = DateTime.UtcNow,
            };
            _orderRepository.SaveOrderEvent(orderPlacedEvent);
            await _bus.Publish(orderPlacedEvent);
        }
        
        public async Task ManageOrder(PaymentCreatedEvent @event)
        {
            Console.WriteLine($"Payment created for order {@event.OrderId}");
            _orderRepository.SaveOrderEvent(@event);

            var orderPackagedEvent = new OrderPackagedEvent(@event.OrderId);
            _orderRepository.SaveOrderEvent(orderPackagedEvent);

            var aggregate = _orderRepository.GetOrderById(orderPackagedEvent.OrderId);

            await _bus.Publish(
                new OrderReadyForShippingEvent(@event.OrderId)
                {
                    Timestamp = DateTime.UtcNow,
                    Order = aggregate
                });
        }
    }
}
