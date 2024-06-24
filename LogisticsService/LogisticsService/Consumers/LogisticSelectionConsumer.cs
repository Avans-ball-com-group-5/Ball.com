using LogisticsDomain.Events.Input;
using LogisticsDomain.Events.Output;
using LogisticsService.Handlers;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Consumers
{
    public class LogisticSelectionConsumer : IConsumer<LogisticSelectionEvent>
    {
        private readonly LogisticsHandler _logisticsHandler;

        public LogisticSelectionConsumer(LogisticsHandler logisticsHandler)
        {
            _logisticsHandler = logisticsHandler;
        }

        public async Task Consume(ConsumeContext<LogisticSelectionEvent> context)
        {
            await _logisticsHandler.ShipOrder(context.Message);
        }
    }
}
