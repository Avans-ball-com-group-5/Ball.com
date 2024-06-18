using System.Text.Json.Serialization;

namespace PaymentDomain.Events.Input
{
    [Serializable]
    public class OrderPlacedEvent
    {
        public OrderPlacedEvent(Guid orderId, string message)
        {
            OrderId = orderId;
            Message = message;
        }

        [JsonPropertyName("orderId")]
        public Guid OrderId { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
