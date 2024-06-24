using Domain.Events;
using MassTransit;
using OrderService.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Consumers
{
    public class PlaceOrderConsumer : IConsumer<PlaceOrderEvent>
    {
        private readonly OrderHandler _orderPublisher;
        public PlaceOrderConsumer(OrderHandler orderPublisher)
        {
            _orderPublisher = orderPublisher;
        }
        public async Task Consume(ConsumeContext<PlaceOrderEvent> context)
        {
            Console.WriteLine("Sending request to place order...");
            await _orderPublisher.PlaceOrder(context.Message);
        }
    }
}
