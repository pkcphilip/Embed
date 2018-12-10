using System.Data.Entity;
using Embed.Core.Entities;

namespace Embed.Persistance
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }
    }
}