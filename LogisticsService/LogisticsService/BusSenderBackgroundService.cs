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
            var orderGuid = Guid.NewGuid();
            var logisticsGuid = Guid.NewGuid();
            do
            {
                // TODO: events to be artificially pushed to the bus
                await bus.Publish(new OrderReadyForShippingEvent()
                {
                    Id = Guid.NewGuid(),
                    Order = new Order() { 
                        Id = orderGuid,
                        CreatedAt = DateTime.Now,
                        LogisticsCompany = new LogisticsCompany { Id = logisticsGuid, Location = "Breda", Name = "MailNL", PricePerKm = 0.5m },
                        LogisticsCompanyId = logisticsGuid,
                        PaymentId = Guid.NewGuid()
                    },
                    Timestamp = DateTime.Now,
                    OrderId = orderGuid
                });
                /*
                var sendEndpoint = await bus.GetSendEndpoint(new Uri("queue:test-test-test"));
                await sendEndpoint.Send(new OrderReadyForShippingEvent()
                {
                    Id = Guid.NewGuid(),
                    Order = new Order()
                    {
                        Id = orderGuid,
                        CreatedAt = DateTime.Now,
                        LogisticsCompany = new LogisticsCompany { Id = logisticsGuid, Location = "Breda", Name = "MailNL", PricePerKm = 0.5m },
                        LogisticsCompanyId = logisticsGuid,
                        PaymentId = Guid.NewGuid()
                    },
                    Timestamp = DateTime.Now,
                    OrderId = orderGuid
                });*/

                await Task.Delay(30000, stoppingToken);
            } while (true);
        }
    }
}
