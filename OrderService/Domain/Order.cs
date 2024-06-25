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
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public Guid PaymentId { get; set; }
        public Dictionary<Guid, int> Items { get; set; } = new();
        public void Apply(PlaceOrderEvent @event)
        {
            this.Id = @event.Order.Id;
            this.Items = @event.Order.Items;
        }
        public void Apply(OrderPlacedEvent @event)
        {
            CreatedAt = @event.Timestamp;
        }
        public void Apply(PaymentCreatedEvent @event)
        {
            PaymentId = @event.PaymentId;
        }
        public void Apply(OrderPackagedEvent @event)
        {
        }

        public override string ToString()
        {
            return $"Id: {Id}, CreatedAt: {CreatedAt}, PaymentId: {PaymentId}, Items: {string.Join(", ", Items.Select(i => $"{{ Id: {i.Id}, Amount: {i.Amount} }}"))}";
        }
    }
}
