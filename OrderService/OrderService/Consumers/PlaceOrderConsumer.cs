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
        private readonly OrderEventHandler _handler;
        public PlaceOrderConsumer(OrderEventHandler handler)
        {
            _handler = handler;
        }
        public async Task Consume(ConsumeContext<PlaceOrderEvent> context)
        {
            Console.WriteLine("Sending request to place order...");
            await _handler.PlaceOrder(context.Message);
        }
    }
}
