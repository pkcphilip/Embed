using Embed.Core.Abstract;
using System;

namespace Embed.Persistance.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }

        void Complete();
    }
}