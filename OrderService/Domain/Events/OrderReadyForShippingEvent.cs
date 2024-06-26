using System.Text.Json.Serialization;

namespace Domain.Events
{
    public class OrderReadyForShippingEvent : OrderBaseEvent
    {
        public OrderReadyForShippingEvent(Guid orderId) : base(orderId)
        {
        }
        [JsonPropertyName("order")]
        public Order Order { get; set; } = new();
    }
}
