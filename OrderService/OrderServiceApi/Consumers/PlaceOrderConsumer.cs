using Domain.Events;
using MassTransit;
using OrderServiceApi.Handlers;

namespace OrderServiceApi.Consumers
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
