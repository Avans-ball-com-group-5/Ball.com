using ProductDomain;

namespace Domain
{
    public interface IProductRepository
    {
        Product GetProductById(Guid id);
        Product AddProduct(Product product);
    }
}
