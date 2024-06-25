using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IOrderQueryHandler
    {
        Order GetOrderById(Guid orderId);
        Order GetAggregateById(Guid orderId);
    }
}
