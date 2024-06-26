using Domain.Events;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace CustomerServiceApi
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
                await bus.Publish(new RegisterCustomerServiceTicket(Guid.NewGuid(), "message"));
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            } while (true);
        }
    }
}
