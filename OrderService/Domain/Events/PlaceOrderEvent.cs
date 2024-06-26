using System.Text.Json.Serialization;

namespace Domain.Events
{
    public class PlaceOrderEvent : OrderBaseEvent
    {
        public PlaceOrderEvent(Guid orderId) : base(orderId)
        {
        }

        [JsonPropertyName("order")]
        public Order Order { get; set; } = new();
    }
}
