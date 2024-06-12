using System.Text.Json.Serialization;

namespace OrderDomain.Events
{
    public class OrderCreatedEvent
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("order")]
        public Order Order { get; set; }
        [JsonPropertyName("orderDate")]
        public DateTime OrderDate { get; set; }
        [JsonPropertyName("customer")]
        public Customer Customer { get; set; } = new Customer();
    }
}
