using System.Text.Json.Serialization;

namespace Domain.Events
{
    [Serializable]
    public class RegisterCustomerServiceTicket
    {
        public RegisterCustomerServiceTicket(Guid id, string name, string email, string phone, string message)
        {
            CustomerId = id;
            Name = name;
            Email = email;
            Phone = phone;
            Message = message;
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
    }
}
