using System.Text.Json.Serialization;

namespace Domain.Events
{
    public class OrderBaseEvent
    {
        private Guid _orderId;
        private string _eventType;
        public OrderBaseEvent(Guid orderId)
        {
            _orderId = orderId;
            _eventType = GetType().Name;
        }
        public OrderBaseEvent()
        {
        }
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonPropertyName("orderId")]
        public Guid OrderId { get => _orderId; set => _orderId = value; }
        [JsonPropertyName("eventType")]
        public string EventType { get => _eventType; set => _eventType = value; }
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonPropertyName("eventData")]
        public string EventData { get; set; }
    }
}
