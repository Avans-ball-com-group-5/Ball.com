using System.Text.Json.Serialization;

namespace Domain.Events
{
    public class ProductServiceRegistered
    {
        [JsonPropertyName("productId")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("company")]
        public string Company { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("stock")]
        public int Stock { get; set; }

        public ProductServiceRegistered(Guid id, string name, string description, string company, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Description = description;
            Company = company;
            Price = price;
            Stock = stock;
        }
    }
}
