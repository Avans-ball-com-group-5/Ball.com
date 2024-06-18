using System.Text.Json.Serialization;

namespace OrderDomain.Events
{
    public class OrderPlacedEvent
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("order")]
        public Order Order { get; set; } = new();
        [JsonPropertyName("orderDate")]
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
