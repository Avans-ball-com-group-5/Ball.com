using CustomerDomain.Events.Input;
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
                await bus.Publish(new RegisterCustomerService(Guid.NewGuid(), "John Doe", "email", "phone", "message"));
                await Task.Delay(30000, stoppingToken);
            } while (true);
        }
    }
}
