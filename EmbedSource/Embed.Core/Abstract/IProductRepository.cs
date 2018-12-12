using System.Collections.Generic;
using Embed.Core.Entities;

namespace Embed.Core.Abstract
{
    /// <summary>
    /// Represents the product repository class.
    /// Mediates between the domain and data mapping layers using a collection-like interface for accessing domain objects.
    /// (ref: http://martinfowler.com/eaaCatalog/repository.html)
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Inserts new product entity into the database.
        /// </summary>
        /// <param name="product">product entity.</param>
        /// <returns>The added entity.</returns>
        Product Add(Product product);

        /// <summary>
        /// Get a collection of all product entities from database.
        /// </summary>
        /// <returns>The collection of product entities.</returns>
        IEnumerable<Product> GetAllProducts();


        /// <summary>
        /// Get product entity from database based on product id.
        /// </summary>
        /// <param name="productid">The product id of the entity.</param>
        /// <returns>The product entity.</returns>
        Product GetProduct(long productid);

        /// <summary>
        /// Get product entities from database based on list of product id.
        /// </summary>
        /// <param name="productIds">The list of product id.</param>
        /// <returns>The collection of product entities that matched the product id.</returns>
        IEnumerable<Product> GetProductsByIds(IList<long> productIds);
    }
}