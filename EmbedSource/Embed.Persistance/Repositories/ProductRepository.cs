using Embed.Core.Abstract;
using Embed.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Embed.Persistance.Repositories
{
    /// <summary>
    /// Represents the product repository class.
    /// Mediates between the domain and data mapping layers using a collection-like interface for accessing domain objects.
    /// (ref: http://martinfowler.com/eaaCatalog/repository.html)
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// Defines the injected context to the repository.
        /// </summary>
        private readonly IApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of <see cref="ProductRepository"/> class.
        /// </summary>
        public ProductRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get product entities from database based on list of product id.
        /// </summary>
        /// <param name="productIds">The list of product id.</param>
        /// <returns>The collection of product entities that matched the product id.</returns>
        public IEnumerable<Product> GetProductsByIds(IList<long> productIds)
        {
            if (productIds == null || productIds.Count == 0)
                return null;

            return _context.Products.Where(p => productIds.Contains(p.Id)).ToList();
        }

        /// <summary>
        /// Get product entity from database based on product id.
        /// </summary>
        /// <param name="productid">The product id of the entity.</param>
        /// <returns>The product entity.</returns>
        public Product GetProduct(long productid)
        {
            return _context.Products.SingleOrDefault(p => p.Id == productid);
        }

        /// <summary>
        /// Get a collection of all product entities from database.
        /// </summary>
        /// <returns>The collection of product entities.</returns>
        public IEnumerable<Product> GetAllProducts()
        {
            return  _context.Products.ToList();
        }

        /// <summary>
        /// Inserts new product entity into the database.
        /// </summary>
        /// <param name="product">product entity.</param>
        /// <returns>The added entity.</returns>
        public Product Add(Product product)
        {
            return _context.Products.Add(product);
        }
    }
}
