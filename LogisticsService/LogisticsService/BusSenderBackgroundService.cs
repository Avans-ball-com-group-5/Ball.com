using Domain.Events;
using Domain.Models;
using MassTransit;
using Microsoft.Extensions.Hosting;

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
            var logisticsGuid = Guid.NewGuid();
            do
            {
                var orderGuid = Guid.NewGuid();
                // TODO: events to be artificially pushed to the bus
                await bus.Publish(new OrderReadyForShippingEvent()
                {
                    Id = Guid.NewGuid(),
                    Order = new Order() { 
                        Id = orderGuid,
                        CreatedAt = DateTime.Now,
                        LogisticsCompanyId = logisticsGuid,
                        PaymentId = Guid.NewGuid(),
                        Items = new List<ItemRef>()
                        {
                            new ItemRef()
                            {
                                Amount = 1,
                                Name = "Paprika Pringles",
                            },
                            new ItemRef()
                            {
                                Amount = 2,
                                Name = "Paprika Doritos"
                            }
                        }
                    },
                    Timestamp = DateTime.Now,
                    OrderId = orderGuid,
                });

                await Task.Delay(30000, stoppingToken);
            } while (true);
        }
    }
}
