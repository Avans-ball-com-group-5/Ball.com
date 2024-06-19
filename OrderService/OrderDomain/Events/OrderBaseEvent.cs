namespace OrderDomain.Events
{
    public abstract class OrderBaseEvent
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string EventType { get; set; }
        public DateTime Timestamp { get; set; }
        public string EventData { get; set; }
    }
}
