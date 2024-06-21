using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService
{
    public class BusSenderBackgroundService : BackgroundService
    {
        private readonly IBus bus;

        public BusSenderBackgroundService(IBus bus)
        {
            this.bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                // TODO: events to be artificially pushed to the bus
                // await bus.Publish(new OrderPlacedEvent(Guid.NewGuid(), false));
                // await bus.Publish(new OrderPlacedEvent(Guid.NewGuid(), true));
                await Task.Delay(30000, stoppingToken);
            } while (true);
        }
    }
}
