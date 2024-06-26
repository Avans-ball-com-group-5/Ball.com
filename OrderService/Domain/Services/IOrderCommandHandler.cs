using Domain.Events;

namespace Domain.Services
{
    public interface IOrderCommandHandler
    {
        void SaveOrderEvent(OrderBaseEvent eventBase);
        void SaveOrder(Order order);
    }
}
