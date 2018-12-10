using Embed.Core.Abstract;
using Embed.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Embed.Persistance.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IApplicationDbContext _context;

        public ProductRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProductsByIds(IList<long> productIds)
        {
            if (productIds == null || productIds.Count == 0)
                return null;

            return _context.Products.Where(p => productIds.Contains(p.Id)).ToList();
        }

        public Product GetProduct(long productid)
        {
            return _context.Products.SingleOrDefault(p => p.Id == productid);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return  _context.Products.ToList();
        }

        public Product Add(Product product)
        {
            return _context.Products.Add(product);
        }

        //public bool VerifyAllProductIdsAreValid(IList<long> productIds)
        //{
        //    if (productIds == null || productIds.Count == 0)
        //        return false;

        //    var existingIds = _context.Products.Select(p => p.Id).ToList();

        //    return productIds.Any(i => !existingIds.Contains(i));
        //}

    }
}
