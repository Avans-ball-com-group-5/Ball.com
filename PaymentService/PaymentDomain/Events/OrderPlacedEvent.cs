using System.Text.Json.Serialization;

namespace Domain.Events
{
    [Serializable]
    public class OrderPlacedEvent
    {
        public OrderPlacedEvent(Guid orderId, bool isAfterPay)
        {
            OrderId = orderId;
            IsAfterPay = isAfterPay;
        }
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("orderId")]
        public Guid OrderId { get; set; }

        [JsonPropertyName("isAfterPay")]
        public bool IsAfterPay { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}