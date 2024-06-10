using Domain.Events;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace CustomerService
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
                await bus.Publish(new RegisterCustomerService("43a6b431-1fd7-4649-ba8c-ee0f6e26cb9a", "John Doe", "email", "phone", "message"));
                await Task.Delay(30000, stoppingToken);
            } while (true);
        }
    }
}
