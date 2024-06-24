using Domain.Events;
using LogisticsService.Handlers;
using MassTransit;

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
