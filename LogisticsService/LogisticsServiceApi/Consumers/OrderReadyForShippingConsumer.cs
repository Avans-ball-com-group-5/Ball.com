using Domain.Events;
using LogisticsServiceApi.Handlers;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsServiceApi.Consumers
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
            await _logisticsHandler.ManageLogistics(context.Message);
        }
    }
}
