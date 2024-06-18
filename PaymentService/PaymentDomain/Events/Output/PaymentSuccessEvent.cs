using System.Text.Json.Serialization;

namespace PaymentDomain.Events.Output
{
    [Serializable]
    public class PaymentSuccessEvent
    {
        [JsonPropertyName("orderId")]
        public Guid OrderId { get; set; } = Guid.NewGuid();

        [JsonPropertyName("paymentId")]
        public Guid PaymentId { get; set; } = Guid.NewGuid();
    }
}
