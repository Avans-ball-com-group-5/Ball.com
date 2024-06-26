using System.Text.Json.Serialization;

namespace Domain.Events
{
    [Serializable]
    public class PayAfterPayRequest
    {
        [JsonPropertyName("paymentId")]
        public Guid PaymentId { get; set; }
    }
}