using MassTransit;
using Microsoft.Extensions.Hosting;
using ProductDomain.Events.Input;


namespace ProductService
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
                await bus.Publish(new RegisterProductService(Guid.NewGuid(), "Beer", "Tasty beer", "t'ij", 2, 50));
                await Task.Delay(30000, stoppingToken);
            } while (true);
        }
    }
}
