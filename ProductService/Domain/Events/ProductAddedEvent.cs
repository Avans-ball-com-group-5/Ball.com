using System.Text.Json.Serialization;

namespace Domain.Events
{
    public class ProductAddedEvent
    {
        public ProductAddedEvent(Guid productId, string name, decimal price)
        {
            ProductId = productId;
            Name = name;
            Price = price;
        }

        [JsonPropertyName("productId")]
        public Guid ProductId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

    }
}
