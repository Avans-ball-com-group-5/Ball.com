using Domain.Events;
using Domain.Models;
using Domain.Services;
using MassTransit;
using MassTransit.Transports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Handlers
{
    public class LogisticsHandler
    {
        private readonly IBus _bus;
        private readonly ILogisticsRepository _logisticsRepository;
        private readonly string divider = "----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
        public LogisticsHandler(IBus bus, ILogisticsRepository logisticsRepository)
        {
            _bus = bus;
            _logisticsRepository = logisticsRepository;
        }

        public async Task ManageLogistics(OrderReadyForShippingEvent orderEvent)
        {
            // Example logic: select the cheapest logistics company for the order
            LogisticsCompany logisticsCompany = _logisticsRepository.GetCheapestLogisticsCompany();

            // Perform actions with the selected logistics company and the order details
            Console.WriteLine($"{divider}\n\n\tOrder {orderEvent.OrderId} will be shipped by {logisticsCompany.Name} with price per km:\t{logisticsCompany.PricePerKm}\n\n{divider}");

            Order order = orderEvent.Order;
            order.LogisticsCompany = logisticsCompany;

            LogisticSelectionEvent logisticEvent = new LogisticSelectionEvent()
            {
                LogisticsCompanyId = logisticsCompany.Id,
                Order = order,
                OrderId = order.Id
            };

            await _bus.Publish(logisticEvent);
        }

        public async Task ShipOrder(LogisticSelectionEvent logisticSelectionEvent) {
            Tracking tracking = new Tracking()
            {
                Order = logisticSelectionEvent.Order,
                OrderId = logisticSelectionEvent.OrderId,
                Status = Status.Left_Ball_Com_Warehouse
            };

            OrderShippedEvent shippedEvent = new OrderShippedEvent()
            {
                LogisticsCompanyId = logisticSelectionEvent.LogisticsCompanyId,
                Tracking = tracking,
                TrackingId = tracking.Id,
            };

            LogisticsCompany logisticsCompany = _logisticsRepository.GetLogisticsCompanyById(logisticSelectionEvent.LogisticsCompanyId);

            Console.WriteLine($"{divider}\n\n\tOrder {shippedEvent.Tracking.OrderId} will be shipped under tracking ID {shippedEvent.TrackingId}.\n\n\tThe order is leaving the Ball.Com warehouse.\n\n\tFor more information look at the {logisticsCompany.Name} website:\t{logisticsCompany.Website}\n\n{divider}");

            await _bus.Publish(shippedEvent);
        }

        public async Task ManageTracking(OrderShippedEvent orderShippedEvent)
        {
            Tracking tracking = orderShippedEvent!.Tracking;

            OrderTrackedEvent orderTrackedEvent = new OrderTrackedEvent()
            {
                Tracking = tracking,
                TrackingId = tracking.Id
            };

            Console.WriteLine($"{divider}\n\n\tOrder number:\t{tracking.OrderId}\n\n\tOrder Status:\t{tracking.Status}");
            Console.WriteLine("\n\tProducts:\tName\t\t\t\tAmount");
            foreach(ItemRef item in tracking.Order.Items)
            {
                Console.WriteLine($"\t\t\t{item.Name}\t\t×{item.Amount}");
            }

            await _bus.Publish(orderTrackedEvent);
            var sendEndpoint = await _bus.GetSendEndpoint(new Uri("queue:tracked-order-event-queue"));
            await sendEndpoint.Send(orderTrackedEvent);
        }
    }
}
