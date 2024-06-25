using Domain;
using Domain.Events;
using Domain.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Consumers
{
    public class OrderDenormalizerConsumer : IConsumer<OrderReadyForShippingEvent>
    {
        IOrderCommandHandler _orderCommandHandler;
        IOrderQueryHandler _orderQueryHandler;
        public OrderDenormalizerConsumer(IOrderCommandHandler orderCommandHandler, IOrderQueryHandler orderQueryHandler)
        {
            _orderCommandHandler = orderCommandHandler;
            _orderQueryHandler = orderQueryHandler;
        }
        public async Task Consume(ConsumeContext<OrderReadyForShippingEvent> context)
        {
            var @event = context.Message;
            Order aggregate = _orderQueryHandler.GetAggregateById(@event.OrderId);
            _orderCommandHandler.SaveOrder(aggregate);
            await Task.CompletedTask;
        }
    }
}
