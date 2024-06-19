using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderDomain.Events
{
    public class OrderPackagedEvent : OrderBaseEvent
    {
        [JsonPropertyName("items")]
        public List<ItemRef> Items { get; set; } = new();
    }
}
