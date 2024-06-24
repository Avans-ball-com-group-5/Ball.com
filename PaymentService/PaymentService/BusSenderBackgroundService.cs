using MassTransit;
using Microsoft.Extensions.Hosting;
using Domain.Events;

namespace PaymentService
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
                await bus.Publish(new OrderPlacedEvent(Guid.NewGuid(), false));
                await bus.Publish(new OrderPlacedEvent(Guid.NewGuid(), true));
                await Task.Delay(30000, stoppingToken);
            } while (true);
        }
    }
}