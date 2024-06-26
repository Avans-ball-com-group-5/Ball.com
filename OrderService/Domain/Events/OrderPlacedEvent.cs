namespace Domain.Events
{
    public class OrderPlacedEvent : OrderBaseEvent
    {
        public OrderPlacedEvent(Guid orderId) : base(orderId)
        {
        }
    }
}
