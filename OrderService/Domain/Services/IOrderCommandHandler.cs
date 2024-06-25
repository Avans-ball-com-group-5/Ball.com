using Domain;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IOrderCommandHandler
    {
        void SaveOrderEvent(OrderBaseEvent eventBase);
        void SaveOrder(Order order);
    }
}
