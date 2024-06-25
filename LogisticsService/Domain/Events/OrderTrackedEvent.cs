using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class OrderTrackedEvent
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.Now;
        [JsonPropertyName("trackingId")]
        public Guid TrackingId { get; set; }
        [JsonPropertyName("tracking")]
        public Tracking? Tracking { get; set; }
    }
}
