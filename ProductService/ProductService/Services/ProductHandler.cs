using Domain;
using Domain.Events;
using MassTransit;
using ProductDomain;

namespace ProductService.Services
{
    public class ProductHandler
    {
        private readonly IBus Bus;
        private readonly IProductRepository _productRepository;

        public ProductHandler(IBus bus, IProductRepository productRepository)
        {
            Bus = bus;
            _productRepository = productRepository;
        }

        public async Task AddProductEvent(AddProductEvent message)
        {
            var product = new Product
            {
                ProductId = message.ProductId,
                Name = message.Name,
                Price = message.Price
            };
            _productRepository.AddProduct(product);

            await Bus.Publish<ProductAddedEvent>(new
            {
                product.ProductId,
                product.Name,
                product.Price
            });
        }

    }
}
