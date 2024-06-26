namespace Domain.Services
{
    public interface IOrderQueryHandler
    {
        Order GetOrderById(Guid orderId);
        Order GetAggregateById(Guid orderId);
    }
}
