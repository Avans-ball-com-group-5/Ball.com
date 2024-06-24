using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Consumers
{
    public  class PlaceOrderConsumerDefinition: ConsumerDefinition<PlaceOrderConsumer>
    {
        public PlaceOrderConsumerDefinition()
        {
            // The endpoint name is the name of the service that will be accepting the message
            EndpointName = "place-order";
        }
    }
}
