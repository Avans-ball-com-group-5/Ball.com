using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderDomain.Events
{
    public class PlaceOrderEvent : OrderBaseEvent
    {
        public PlaceOrderEvent(Guid orderId) : base(orderId)
        {
        }

        [JsonPropertyName("order")]
        public Order Order { get; set; } = new();
    }
}
