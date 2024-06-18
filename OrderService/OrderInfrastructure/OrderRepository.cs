using Cqrs.Events;

namespace OrderInfrastructure
{
    public class OrderRepository
    {
        private readonly IEventStore<IAuthenticationToken> _eventStore;
        public OrderRepository(IEventStore<IAuthenticationToken> eventStore)
        {
            _eventStore = eventStore;
        }

        public Order GetOrder(Guid orderId)
        {
            var events = _eventStore.GetEventsForAggregate(orderId);
            var order = new Order(orderId);
            order.Apply(events);
            return order;
        }
    }
    
    internal interface IAuthenticationToken
    {
    }
}
