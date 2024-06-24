using System.Text.Json.Serialization;

namespace Domain.Events
{
    [Serializable]
    public class PaymentCreatedEvent : OrderBaseEvent
    {
        public PaymentCreatedEvent(Guid orderId) : base(orderId)
        {
        }

        [JsonPropertyName("isCompleted")]
        public bool IsCompleted { get; set; }
        [JsonPropertyName("paymentId")]
        public Guid PaymentId { get; set; }
    }
}
