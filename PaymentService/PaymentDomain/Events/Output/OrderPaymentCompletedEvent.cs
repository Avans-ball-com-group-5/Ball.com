using System.Text.Json.Serialization;

namespace PaymentDomain.Events.Output
{
    [Serializable]
    public class OrderPaymentCompletedEvent
    {
        [JsonPropertyName("orderId")]
        public Guid OrderId { get; set; }

        [JsonPropertyName("paymentId")]
        public Guid PaymentId { get; set; }
    }
}
