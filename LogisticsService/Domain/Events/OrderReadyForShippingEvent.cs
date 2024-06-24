using Domain.Models;
using MassTransit.Transports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class OrderReadyForShippingEvent
    {
        private Guid _orderId;
        private string _eventType;
        public OrderReadyForShippingEvent(Guid orderId)
        {
            _orderId = orderId;
            _eventType = GetType().Name;
        }

        public OrderReadyForShippingEvent()
        {

        }

        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonPropertyName("order")]
        public Order Order { get; set; } = new();
        [JsonPropertyName("orderId")]
        public Guid OrderId { get => _orderId; set => _orderId = value; }
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
