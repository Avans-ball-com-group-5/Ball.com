using System.Text.Json.Serialization;

namespace Domain.Events
{
    [Serializable]
    public class RegisterCustomerService
    {
        public RegisterCustomerService(string id, string name, string email, string phone, string message)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Message = message;
        }

        [JsonPropertyName("id")]
        public string Id { get; set; }

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
