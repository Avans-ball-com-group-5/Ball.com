using MassTransit;
using Microsoft.Extensions.Hosting;
using PaymentDomain.Events.Input;

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
                await bus.Publish(new OrderPlacedEvent(Guid.NewGuid(), "message"));
                await Task.Delay(3000, stoppingToken);
            } while (true);
        }
    }
}
