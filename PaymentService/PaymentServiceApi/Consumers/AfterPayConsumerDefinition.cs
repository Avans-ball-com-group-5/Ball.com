using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServiceApi.Consumers
{
    public class AfterPayConsumerDefinition : ConsumerDefinition<AfterPayConsumer>
    {
        public AfterPayConsumerDefinition()
        {
            EndpointName = "after-pay-completed";
        }
    }
}
