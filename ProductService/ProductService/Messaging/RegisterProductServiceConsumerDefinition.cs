using MassTransit;

namespace ProductService.Messaging
{
    internal class RegisterProductServiceConsumerDefinition : ConsumerDefinition<RegisterProductServiceConsumer>
    {
        public RegisterProductServiceConsumerDefinition()
        {
            EndpointName = "register-product-service";
        }
    }
}
