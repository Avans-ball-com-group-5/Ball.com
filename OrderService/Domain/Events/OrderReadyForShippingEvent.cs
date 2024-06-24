using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class OrderReadyForShippingEvent : OrderBaseEvent
    {
        public OrderReadyForShippingEvent(Guid orderId) : base(orderId)
        {
        }
        [JsonPropertyName("order")]
        public Order Order { get; set; } = new();
    }
}
