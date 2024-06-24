using OrderDomain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderDomain.Services
{
    public interface IOrderRepository
    {
        void SaveOrderEvent<T>(T eventBase) where T : OrderBaseEvent;
        Order GetOrderById(Guid orderId);
    }
}
