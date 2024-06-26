using MassTransit;
using Microsoft.Extensions.Hosting;
using Domain.Events;

namespace PaymentServiceApi
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
                await bus.Publish(new OrderPlacedEvent() { Price = 10.0M, IsAfterPay = false });
                await bus.Publish(new OrderPlacedEvent() { Price = 15.0M, IsAfterPay = true });
                await Task.Delay(30000, stoppingToken);
            } while (true);
        }
    }
}