using Domain;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IBus _bus;
        public ProductController(IProductRepository repository, IBus bus)
        {
            _repository = repository;
            _bus = bus;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IEnumerable<Product>> Get()
        {
            await _bus.Publish(new ProductEvent
            {
                TimeStamp = DateTime.Now,
                Message = "All products retrieved"
            });
            return _repository.GetAll();
        }
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<Product> Get([FromRoute] Guid id)
        {
            await _bus.Publish(new ProductEvent
            {
                TimeStamp = DateTime.Now,
                Message = "All products retrieved"
            });
            return _repository.GetProductById(id);
        }
        [HttpPost]
        [Route("api/[controller]")]
        public async Task Post([FromBody] Product product)
        {
            _repository.AddProduct(product);
            await _bus.Publish(new ProductEvent
            {
                TimeStamp = DateTime.Now,
                Message = $"Product {product} was added"
            });
        }
    }
}