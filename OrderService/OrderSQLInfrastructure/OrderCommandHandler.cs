using Domain;
using Domain.Events;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.Tracing;

namespace OrderSQLInfrastructure
{
    public class OrderCommandHandler : IOrderCommandHandler
    {
        private readonly OrderEventDbContext _context;

        public OrderCommandHandler(OrderEventDbContext context)
        {
            _context = context;
        }

        public void SaveOrderEvent(OrderBaseEvent eventBase)
        {
            eventBase.Id = Guid.NewGuid();
            eventBase.Timestamp = DateTime.UtcNow;
            eventBase.EventData = JsonConvert.SerializeObject(eventBase);

            _context.Events.Add(eventBase);
            _context.SaveChanges();
        }

        public void SaveOrder(Order order)
        {
            // search order with included items property
            var existingOrder = _context.Orders.Include(o => o.Items).FirstOrDefault(o => o.Id == order.Id);
            if (existingOrder == null)
                _context.Orders.Add(order);
            else
            {
                // Update only non-null properties
                foreach (var property in _context.Entry(order).Properties)
                {
                    var value = property.CurrentValue;
                    if (value != null)
                        _context.Entry(existingOrder).Property(property.Metadata.Name).CurrentValue = value;
                }
            }
            _context.SaveChanges();
        }
    }
}