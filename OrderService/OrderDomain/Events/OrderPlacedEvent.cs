using System.Text.Json.Serialization;

namespace OrderDomain.Events
{
    public class OrderPlacedEvent : OrderBaseEvent
    {
        public OrderPlacedEvent(Guid orderId) : base(orderId)
        {
        }
    }
}
