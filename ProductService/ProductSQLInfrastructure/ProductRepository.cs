using Domain;
using ProductDomain;

namespace ProductSQLInfrastructure
{
    public class ProductRepository : IProductRepository
    {
        public readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public Product GetProductById(Guid id)
        {
            return _context.products.FirstOrDefault(p => p.ProductId == id);
        }

        public Product AddProduct(Product product)
        {
            _context.products.Add(product);
            _context.SaveChanges();
            return product;
        }
    }
}