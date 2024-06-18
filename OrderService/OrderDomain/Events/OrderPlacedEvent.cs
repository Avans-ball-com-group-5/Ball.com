using System.Text.Json.Serialization;

namespace OrderDomain.Events
{
    public class OrderPlacedEvent
    {
        [JsonPropertyName("orderId")]
        public Guid OrderId { get; set; }
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
