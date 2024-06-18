using System.Text.Json.Serialization;

namespace PaymentDomain.Events.Input
{
    [Serializable]
    public class OrderPlacedEvent
    {
        public OrderPlacedEvent(Guid orderId, bool isAfterPay)
        {
            OrderId = orderId;
            IsAfterPay = isAfterPay;
        }

        [JsonPropertyName("orderId")]
        public Guid OrderId { get; set; }

        [JsonPropertyName("isAfterPay")]
        public bool IsAfterPay { get; set; }
    }
}