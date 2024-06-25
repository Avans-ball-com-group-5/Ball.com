using Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? PaymentId { get; set; }
        public List<ItemRef>? Items { get; set; }
        public void Apply(OrderBaseEvent @event)
        {
            switch (@event)
            {
                case PlaceOrderEvent e:
                    this.Id = e.OrderId;
                    if (e.Order != null)
                       this.Items = e.Order.Items;
                    break;
                case OrderPlacedEvent e:
                    this.Id = e.OrderId;
                    this.CreatedAt = e.Timestamp;
                    break;
                case PaymentCreatedEvent e:
                    this.Id = e.OrderId;
                    this.PaymentId = e.PaymentId;
                    break;
                case OrderPackagedEvent e:
                    break;
            }
        }

        public override string ToString()
        {
            return $"Id: {Id}, CreatedAt: {CreatedAt}, PaymentId: {PaymentId}, Items: {string.Join(", ", Items ?? new List<ItemRef>())}";
        }
    }
    public class ItemRef
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Amount { get; set; }
    }
}
