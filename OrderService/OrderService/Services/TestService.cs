using Domain.Events;
using Domain.Services;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace OrderService.Services
{
    public class TestService : BackgroundService
    {
        private readonly IBus bus;
        private readonly IOrderRepository repository;

        public TestService(IBus bus, IOrderRepository repository)
        {
            this.bus = bus;
            this.repository = repository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var orderId = Guid.NewGuid();
            
            PlaceOrderEvent @event = new(orderId)
            {
                Timestamp = DateTime.UtcNow,
                Order = new()
                {
                    Id = orderId,
                }
            };
            Console.WriteLine("Sending PlaceOrderEvent to bus...");
            await bus.Publish(@event, stoppingToken);
            // await Task.Delay(3000, stoppingToken);

            // PaymentCompletedEvent @event2 = new(orderId)
            // {
            //     Timestamp = DateTime.UtcNow,
            //     IsCompleted = true,
            //     PaymentId = Guid.NewGuid()
            // };
            // Console.WriteLine("Sending PaymentCompletedEvent to bus...");
            // await bus.Publish(@event2, stoppingToken);
            // await Task.Delay(3000, stoppingToken);

            // Order order = repository.GetOrderById(orderId);
            // Console.WriteLine($"Aggregate of order events: {order}");
        }
    }
}
