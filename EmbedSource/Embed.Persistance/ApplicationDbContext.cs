using Embed.Core.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embed.Persistance
{
    /// <summary>
    /// The application DB context. Inherit from IdentityDbContext to integrate ASP.Net identity to the system.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>, IApplicationDbContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationDbContext"/> class.
        /// </summary>
        public ApplicationDbContext() : base("EmbedDbContext")
        {
        }

        /// <summary>
        /// Gets or sets the DbSet of products.
        /// </summary>
        public DbSet<Product> Products { get; set; }
    }
}
