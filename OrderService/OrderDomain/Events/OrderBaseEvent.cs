namespace OrderDomain.Events
{
    public class OrderBaseEvent
    {
        private Guid _orderId;
        private string _eventType;
        public OrderBaseEvent(Guid orderId)
        {
            _orderId = orderId;
            _eventType = GetType().Name;
        }
        public OrderBaseEvent()
        {
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get => _orderId; set => _orderId = value; }
        public string EventType { get => _eventType; set => _eventType = value; }
        public DateTime Timestamp { get; set; }
        public string EventData { get; set; }
    }
}
