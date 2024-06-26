using Domain.Events;
using Domain.Models;
using Domain.Services;
using LogisticsSQLInfrastructure;
using MassTransit;
using MassTransit.Transports;
using Microsoft.EntityFrameworkCore;
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
        private readonly ITrackingRepository _trackingRepository;
        private readonly string divider = "----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
        public LogisticsHandler(IBus bus, ILogisticsRepository logisticsRepository, ITrackingRepository trackingRepository)
        {
            _bus = bus;
            _logisticsRepository = logisticsRepository;
            _trackingRepository = trackingRepository;
        }

        public async Task ManageLogistics(OrderReadyForShippingEvent orderEvent)
        {
            var result = _logisticsRepository.GetCheapestLogisticsCompany();
            var logisticsCompany = result.Company;
            var randomDistance = result.Distance;

            Order order = orderEvent.Order;

            Console.WriteLine($"{divider}\n\n\tOrder {orderEvent.OrderId} will be shipped by {logisticsCompany.Name} with price:\t${randomDistance*logisticsCompany.PricePerKm} ({logisticsCompany.PricePerKm}$/km)\n\n{divider}");


            LogisticSelectionEvent logisticEvent = new LogisticSelectionEvent()
            {
                LogisticsCompanyId = logisticsCompany.Id,
                Order = order,
                OrderId = order.Id
            };

            await _bus.Publish(logisticEvent);
        }

        public async Task ShipOrder(LogisticSelectionEvent logisticSelectionEvent)
        {
            var order = logisticSelectionEvent.Order;

            order.LogisticsCompanyId = logisticSelectionEvent.LogisticsCompanyId;

            Tracking tracking = new Tracking()
            {
                Order = order,
                OrderId = logisticSelectionEvent.OrderId,
                Status = Status.Left_Ball_Com_Warehouse
            };

            _trackingRepository.AddTracking(tracking);

            OrderShippedEvent shippedEvent = new OrderShippedEvent()
            {
                LogisticsCompanyId = logisticSelectionEvent.LogisticsCompanyId,
                TrackingId = tracking.Id
            };

            LogisticsCompany logisticsCompany = _logisticsRepository.GetLogisticsCompanyById(logisticSelectionEvent.LogisticsCompanyId);

            Console.WriteLine($"{divider}\n\n\tOrder {tracking.OrderId} will be shipped under tracking ID {shippedEvent.TrackingId}.\n\n\tThe order is leaving the Ball.Com warehouse.\n\n\tFor more information look at the {logisticsCompany.Name} website:\t{logisticsCompany.Website}\n\n{divider}");

            await _bus.Publish(shippedEvent);
        }

        public async Task ManageTracking(OrderShippedEvent orderShippedEvent)
        {
            Tracking tracking = _trackingRepository.GetTrackingById(orderShippedEvent.TrackingId);

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
        }
    }
}
