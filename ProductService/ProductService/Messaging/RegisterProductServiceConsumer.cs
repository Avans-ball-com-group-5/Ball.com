
using MassTransit;
using ProductDomain.Events.Input;
using ProductService.Services;

namespace ProductService.Messaging
{
    public class RegisterProductServiceConsumer : IConsumer<RegisterProductService>
    {
        private readonly ProductHandler _productHandler;
        public RegisterProductServiceConsumer(ProductHandler productHandler)
        {
            _productHandler = productHandler;
        }

        public async Task Consume(ConsumeContext<RegisterProductService> context)
        {
            
            await _productHandler.RegisterProductService(context.Message);
        }
    }
}
