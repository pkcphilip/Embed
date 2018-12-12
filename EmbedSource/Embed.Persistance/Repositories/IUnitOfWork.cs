using Embed.Core.Abstract;
using System;

namespace Embed.Persistance.Repositories
{
    /// <summary>
    /// Represents the base class of unit of work. This is a simple implementation of the unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// The repository of products.
        /// </summary>
        IProductRepository Products { get; }

        /// <summary>
        /// Commit and save all changes to DB.
        /// </summary>
        void Complete();
    }
}