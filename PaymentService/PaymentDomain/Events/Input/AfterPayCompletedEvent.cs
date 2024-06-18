using System.Text.Json.Serialization;

namespace PaymentDomain.Events.Input
{
    [Serializable]
    public class PayAfterPayRequest
    {
        [JsonPropertyName("paymentId")]
        public Guid PaymentId { get; set; }
    }
}
