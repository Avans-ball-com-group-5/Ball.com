using System.Text.Json.Serialization;

namespace ProductDomain
{
    public class Product
    {
        [JsonPropertyName("productId")]
        public Guid ProductId { get; set; } = Guid.NewGuid();

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; } = 0.0m;
    }
}
