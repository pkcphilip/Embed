using System.Collections.Generic;
using System.Threading.Tasks;
using Embed.Core.Entities;

namespace Embed.Core.Abstract
{
    public interface IProductRepository
    {
        Product Add(Product product);

        IEnumerable<Product> GetAllProducts();

        Product GetProduct(long productid);

        IEnumerable<Product> GetProducts(IList<long> productIds);
    }
}