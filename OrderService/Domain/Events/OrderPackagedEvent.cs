namespace Domain.Events
{
    public class OrderPackagedEvent : OrderBaseEvent
    {
        public OrderPackagedEvent(Guid orderId) : base(orderId)
        {
        }
    }
}
