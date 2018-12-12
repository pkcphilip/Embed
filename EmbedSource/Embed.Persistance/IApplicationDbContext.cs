using System.Data.Entity;
using Embed.Core.Entities;

namespace Embed.Persistance
{
    /// <summary>
    /// The application DB context. Inherit from IdentityDbContext to integrate ASP.Net identity to the system.
    /// </summary>
    public interface IApplicationDbContext
    {
        /// <summary>
        /// Gets or sets the DbSet of products.
        /// </summary>
        DbSet<Product> Products { get; set; }
    }
}