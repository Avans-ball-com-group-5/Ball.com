using System.Text.Json.Serialization;

namespace ProductDomain
{
    public class Product
    {
        [JsonPropertyName("productId")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("company")]
        public string Company { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; } = 0.0m;

        [JsonPropertyName("stock")]
        public int Stock { get; set; } = 0;


    }
}
