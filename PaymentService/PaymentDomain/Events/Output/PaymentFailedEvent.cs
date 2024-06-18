using System.Text.Json.Serialization;

namespace PaymentDomain.Events.Output
{
    [Serializable]
    public class PaymentFailedEvent
    {
        [JsonPropertyName("orderId")]
        public Guid OrderId { get; set; } = Guid.NewGuid();

        [JsonPropertyName("reason")]
        public string Reason { get; set; } = "Payment failed";
    }
}
