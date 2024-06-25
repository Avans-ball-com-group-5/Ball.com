using System.Text.Json.Serialization;

namespace Domain.Events
{
    [Serializable]
    public class RegisterCustomerServiceTicket
    {
        public RegisterCustomerServiceTicket(Guid id, string message)
        {
            CustomerId = id;
            Message = message;
        }

        [JsonPropertyName("customerId")]
        public Guid CustomerId { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
