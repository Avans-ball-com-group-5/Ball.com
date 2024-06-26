using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsServiceApi.Consumers
{
    public class OrderReadyForShippingDefinition : ConsumerDefinition<OrderReadyForShippingConsumer>
    {
        public OrderReadyForShippingDefinition()
        {
            // The endpoint name is the name of the service that will be accepting the message
            EndpointName = "order-ready-for-shipping";
        }
    }
}
