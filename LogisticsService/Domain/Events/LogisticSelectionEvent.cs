using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class LogisticSelectionEvent
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonPropertyName("logisticsCompany")]
        public LogisticsCompany? LogisticsCompany { get; set; }
        [JsonPropertyName("logisticsCompanyId")]
        public Guid LogisticsCompanyId { get; set; }
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.Now;
        [JsonPropertyName("order")]
        public Order? Order { get; set; }
        [JsonPropertyName("orderId")]
        public Guid OrderId { get; set; }
    }
}
