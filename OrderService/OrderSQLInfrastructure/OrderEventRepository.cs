using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OrderDomain;
using OrderDomain.Events;
using OrderDomain.Services;
using System;
using System.Diagnostics.Tracing;

namespace OrderSQLInfrastructure
{
    public class OrderEventRepository: IOrderRepository
    {
        private readonly OrderEventDbContext _context;

        public OrderEventRepository(OrderEventDbContext context)
        {
            _context = context;
        }

        public void SaveOrderEvent<T>(T eventBase) where T : OrderBaseEvent
        {
            eventBase.Id = Guid.NewGuid();
            eventBase.EventType = eventBase.GetType().Name;
            eventBase.Timestamp = DateTime.UtcNow;
            eventBase.EventData = JsonConvert.SerializeObject(eventBase);

            _context.Events.Add(eventBase);
            _context.SaveChanges();
        }

        private List<object> GetEventsForAggregate(Guid aggregateId)
        {
            var events = _context.Events
                .Where(e => e.OrderId == aggregateId)
                .OrderBy(e => e.Id)
                .Select(e => JsonConvert.DeserializeObject(e.EventData, Type.GetType(e.EventType))).ToList()
                .ToList();

            return events;
        }

        public Order GetOrderById(Guid orderId)
        {
            var order = new Order
            {
                Id = orderId,
            };
            var events = GetEventsForAggregate(orderId);

            foreach (var @event in events)
            {
                order.Apply((dynamic)@event);
            }
            return order;
        }
    }
}