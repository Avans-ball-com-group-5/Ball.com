namespace OrderDomain.Events
{
    public class OrderBaseEvent
    {
        private Guid _orderId;
        public OrderBaseEvent(Guid orderId)
        {
            _orderId = orderId;
        }
        public OrderBaseEvent()
        {
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get => _orderId; }
        public string EventType { get => GetType().Name; }
        public DateTime Timestamp { get; set; }
        public string EventData { get; set; }
    }
}
