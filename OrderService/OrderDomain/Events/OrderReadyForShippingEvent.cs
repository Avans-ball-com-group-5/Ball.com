using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderDomain.Events
{
    public class OrderReadyForShippingEvent
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("order")]
        public Guid OrderId { get; set; }
    }
}
