using Domain.Events;
using LogisticsService.Handlers;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Consumers
{
    public class OrderTrackingConsumer : IConsumer<OrderShippedEvent>
    {
        private readonly LogisticsHandler _logisticsHandler;

        public OrderTrackingConsumer(LogisticsHandler logisticsHandler) {
            _logisticsHandler = logisticsHandler;
        }

        public async Task Consume(ConsumeContext<OrderShippedEvent> context)
        {
            await _logisticsHandler.ManageTracking(context.Message);
        }
    }
}
