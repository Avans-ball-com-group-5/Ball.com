namespace Domain
{
    public class Ticket
    {
        public Ticket(Guid id, string message, Guid customerId)
        {
            Id = id;
            Message = message;
            CustomerId = customerId;
        }

        public Guid Id { get; set; }
        public string Message { get; set; }

        public Guid CustomerId { get; set; }
    }
}
