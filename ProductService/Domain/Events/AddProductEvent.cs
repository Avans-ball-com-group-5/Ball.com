using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class AddProductEvent
    {
        private Guid _productId;

        public AddProductEvent(Guid productId, string name, decimal price)
        {
            _productId = productId;
            Name = name;
            Price = price;
        }

        public AddProductEvent()
        {
        }

        [JsonPropertyName("productId")]
        public Guid ProductId { get; set; } = Guid.NewGuid();

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; } = 0.0m;
    }
}
