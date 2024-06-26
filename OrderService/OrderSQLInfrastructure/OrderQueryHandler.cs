using Domain;
using Domain.Events;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OrderSQLInfrastructure
{
    public class OrderQueryHandler : IOrderQueryHandler
    {
        private readonly OrderEventDbContext _context;
        public OrderQueryHandler(OrderEventDbContext context)
        {
            _context = context;
        }

        // Get order from read model
        public Order GetOrderById(Guid orderId)
        {
            return _context.Orders.Include(x => x.Items)
                .FirstOrDefault(x => x.Id == orderId) 
                ?? throw new Exception($"Order with id: {orderId} not found");
        }

        // Get order by replaying events
        public Order GetAggregateById(Guid orderId)
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

        private static OrderBaseEvent ConvertJsonToOrderBaseObject(string json)
        {
            // convert json to object
            var @object = JsonConvert.DeserializeObject(json);
            // convert to correct derived OrderBaseEvent class
            var jObject = JObject.FromObject(@object);
            var eventType = jObject["EventType"]?.ToString() ?? throw new Exception("EventType not found");
            switch (eventType)
            {
                case "OrderPlacedEvent":
                    var orderPlacedEvent = JsonConvert.DeserializeObject<OrderPlacedEvent>(json);
                    return orderPlacedEvent ?? new(new());
                case "OrderPackagedEvent":
                    return JsonConvert.DeserializeObject<OrderPackagedEvent>(json);
                case "OrderReadyForShippingEvent":
                    return JsonConvert.DeserializeObject<OrderReadyForShippingEvent>(json);
                case "PaymentCreatedEvent":
                    return JsonConvert.DeserializeObject<PaymentCreatedEvent>(json);
                case "PlaceOrderEvent":
                    return JsonConvert.DeserializeObject<PlaceOrderEvent>(json);
                default:
                    throw new Exception("Unknown event type");
            }
        }

        private List<OrderBaseEvent> GetEventsForAggregate(Guid aggregateId)
        {
            var eventsFromDb = _context.Events
                .Where(e => e.OrderId == aggregateId)
                .OrderBy(e => e.Id)
                .ToList();

            var filteredEvents = new List<OrderBaseEvent>();

            foreach (var eventData in eventsFromDb)
            {
                var orderBaseEvent = ConvertJsonToOrderBaseObject(eventData.EventData);

                if (orderBaseEvent != null)
                    filteredEvents.Add(orderBaseEvent);
                else
                    filteredEvents.Add(eventData);
            }
            return filteredEvents;
        }
    }
}
