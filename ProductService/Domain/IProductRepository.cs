using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetProductById(Guid id);
        Product AddProduct(Product product);
    }
}
