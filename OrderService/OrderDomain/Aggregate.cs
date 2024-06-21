using OrderDomain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderDomain
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public Guid PaymentId { get; set; }
        public List<ItemRef> Items { get; set; } = new();
        public void Apply(OrderPlacedEvent @event)
        {
            CreatedAt = @event.Timestamp;
        }
        public void Apply(PaymentCompletedEvent @event)
        {
            PaymentId = @event.PaymentId;
        }
        public void Apply(OrderPackagedEvent @event)
        {
            Items = @event.Items;
        }

        public override string ToString()
        {
            return $"Id: {Id}, CreatedAt: {CreatedAt}, PaymentId: {PaymentId}, Items: {string.Join(", ", Items.Select(i => $"{{ Id: {i.Id}, Amount: {i.Amount} }}"))}";
        }
    }

    public class ItemRef
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Amount { get; set; }
    }
}
