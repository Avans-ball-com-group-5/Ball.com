using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        private static OrderBaseEvent ConvertJsonToOrderBaseObject(string json)
        {
            // convert json to object
            var @object = JsonConvert.DeserializeObject(json);
            // convert to correct derived OrderBaseEvent class
            var jObject = JObject.FromObject(@object);
            var eventType = jObject["EventType"].ToString();
            switch (eventType)
            {
                case "OrderPlacedEvent":
                    return JsonConvert.DeserializeObject<OrderPlacedEvent>(json);
                case "OrderPackagedEvent":
                    return JsonConvert.DeserializeObject<OrderPackagedEvent>(json);
                case "OrderReadyForShippingEvent":
                    return JsonConvert.DeserializeObject<OrderReadyForShippingEvent>(json);
                case "PaymentCompletedEvent":
                    return JsonConvert.DeserializeObject<PaymentCompletedEvent>(json);
                case "OrderCancelledEvent":
                    return JsonConvert.DeserializeObject<PlaceOrderEvent>(json);
                default:
                    throw new Exception("Unknown event type");
            }
        }

        private List<OrderBaseEvent> GetEventsForAggregate(Guid aggregateId)
        {
            var events = _context.Events
                .Where(e => e.OrderId == aggregateId)
                .OrderBy(e => e.Id)
                .Select(e => ConvertJsonToOrderBaseObject(e.EventData))
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