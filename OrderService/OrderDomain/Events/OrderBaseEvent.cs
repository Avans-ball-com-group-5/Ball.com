namespace OrderDomain.Events
{
    public class OrderBaseEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public string EventType { get; set; }
        public DateTime Timestamp { get; set; }
        public string EventData { get; set; }
    }
}
