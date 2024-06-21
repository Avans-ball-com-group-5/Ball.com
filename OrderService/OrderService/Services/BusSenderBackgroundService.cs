using MassTransit;
using Microsoft.Extensions.Hosting;
using OrderDomain;
using OrderDomain.Events;
using OrderDomain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Services
{
    public class BusSenderBackgroundService : BackgroundService
    {
        private readonly IBus bus;
        private readonly IOrderRepository repository;

        public BusSenderBackgroundService(IBus bus, IOrderRepository repository)
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
            await Task.Delay(3000, stoppingToken);

            PaymentCompletedEvent @event2 = new(orderId)
            {
                Timestamp = DateTime.UtcNow,
                IsCompleted = true,
                PaymentId = Guid.NewGuid()
            };
            Console.WriteLine("Sending PaymentCompletedEvent to bus...");
            await bus.Publish(@event2, stoppingToken);

            Order order = repository.GetOrderById(orderId);
            Console.WriteLine($"Aggregate of order events: {order}");
        }
    }
}
