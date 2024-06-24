using LogisticsDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LogisticsDomain.Events.Output
{
    public class OrderShippedEvent
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonPropertyName("logisticsCompany")]
        public LogisticsCompany? LogisticsCompany { get; set; }
        [JsonPropertyName("logisticsCompanyId")]
        public Guid LogisticsCompanyId { get; set; }
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.Now;
        [JsonPropertyName("trackingId")]
        public Guid TrackingId { get; set; }
        [JsonPropertyName("tracking")]
        public Tracking? Tracking { get; set; }
    }
}
