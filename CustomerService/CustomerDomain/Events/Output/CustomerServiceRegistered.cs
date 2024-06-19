using System.Text.Json.Serialization;

namespace CustomerDomain.Events.Output
{
    public class CustomerServiceRegistered
    {
        public CustomerServiceRegistered(Guid customerId, string name, string email, string phone, string message, Guid ticketId)
        {
            CustomerId = customerId;
            Name = name;
            Email = email;
            Phone = phone;
            Message = message;
            TicketId = ticketId;
        }

        [JsonPropertyName("customerId")]
        public Guid CustomerId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("ticketId")]
        public Guid TicketId { get; set; }
    }
}
