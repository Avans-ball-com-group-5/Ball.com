using System.Text.Json.Serialization;

namespace Domain.Events
{
    public class CustomerServiceTicketRegistered
    {
        public CustomerServiceTicketRegistered(Guid customerId, string message, Guid ticketId)
        {
            CustomerId = customerId;
            Message = message;
            TicketId = ticketId;
        }

        [JsonPropertyName("customerId")]
        public Guid CustomerId { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("ticketId")]
        public Guid TicketId { get; set; }
    }
}
