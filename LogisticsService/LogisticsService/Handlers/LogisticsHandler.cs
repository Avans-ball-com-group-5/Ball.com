using LogisticsDomain;
using LogisticsDomain.Events.Input;
using LogisticsDomain.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Handlers
{
    public class LogisticsHandler
    {
        private readonly IBus _bus;
        private readonly ILogisticsRepository _logisticsRepository;
        public LogisticsHandler(IBus bus, ILogisticsRepository logisticsRepository)
        {
            _bus = bus;
            _logisticsRepository = logisticsRepository;
        }
        // TODO: Tasks added and finished 
        /*
        public async Task LogisticsSelection(PlaceOrderEvent @event)
        {
            LogisticsCompany logisticsCompany = _logisticsRepository.GetCheapestLogisticsCompany();

            // Order was placed, store event and publish to event bus
            var orderPlacedEvent = new OrderPlacedEvent(@event.OrderId)
            {
                Timestamp = DateTime.UtcNow,
            };

            await _bus.Publish(orderPlacedEvent);
        }*/
    }
}
