using MassTransit;

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