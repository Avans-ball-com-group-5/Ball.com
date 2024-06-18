using System.Text.Json.Serialization;

namespace OrderDomain.Events
{
    [Serializable]
    public class PaymentCompletedEvent
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("isCompleted")]
        public bool IsCompleted { get; set; }
        [JsonPropertyName("order")]
        public Guid OrderId { get; set; }
    }
}
