using ProductService.Services;
using Domain.Events;
using MassTransit;

namespace ProductService.Messaging
{
    public class ProductAddedEventConsumer : IConsumer<ProductAddedEvent>
    {
        private readonly ProductHandler _productHandler;
        
        public ProductAddedEventConsumer(ProductHandler productHandler)
        {
            _productHandler = productHandler;
        }

        public async Task Consume(ConsumeContext<ProductAddedEvent> context)
        {
            await _productHandler.HandleProductAdded(context.Message);
        }
    }
}
