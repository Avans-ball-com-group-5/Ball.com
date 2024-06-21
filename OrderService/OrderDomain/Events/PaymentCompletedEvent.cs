using System.Text.Json.Serialization;

namespace OrderDomain.Events
{
    [Serializable]
    public class PaymentCompletedEvent : OrderBaseEvent
    {
        public PaymentCompletedEvent(Guid orderId) : base(orderId)
        {
        }

        [JsonPropertyName("isCompleted")]
        public bool IsCompleted { get; set; }
        [JsonPropertyName("paymentId")]
        public Guid PaymentId { get; set; }
    }
}
