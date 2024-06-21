using LogisticsDomain.Events.Input;
using LogisticsService.Handlers;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Consumers
{
    public class OrderReadyForShippingConsumer : IConsumer<OrderReadyForShippingEvent>
    {
        private readonly LogisticsHandler _logisticsHandler;
        public OrderReadyForShippingConsumer(LogisticsHandler logisticsHandler)
        {
            _logisticsHandler = logisticsHandler;
        }

        public async Task Consume(ConsumeContext<OrderReadyForShippingEvent> context)
        {
            // TODO: Consume incoming event
            Console.WriteLine("Starting the shipment...");
            // await _logisticsHandler.ManageOrder(context.Message);
        }
    }
}
