using MassTransit;

namespace ProductService.Messaging
{
    internal class ProductAddedEventConsumerDefinition : ConsumerDefinition<ProductAddedEventConsumer>
    {
        public ProductAddedEventConsumerDefinition()
        {
            EndpointName = "product-added-event";
        }
    }
}
