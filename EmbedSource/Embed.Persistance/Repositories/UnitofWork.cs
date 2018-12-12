using Embed.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embed.Persistance.Repositories
{
    /// <summary>
    /// Represents the base class of unit of work. This is a simple implementation of the unit of work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The db context for any business transaction.
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// The diposed flag to mark object dispose status.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// The repository of products.
        /// </summary>
        public IProductRepository Products { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context"> The db context.</param>
        public UnitOfWork(ApplicationDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException($"{nameof(context)} was not supplied.");

            _context = context;                                                                                                                                                                                                          
            Products = new ProductRepository(context);
        }

        /// <summary>
        /// Commit and save all changes to DB.
        /// </summary>
        public void Complete()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Dispose the object when out of scope.
        /// </summary>
        /// <param name="disposing">Flag to check if the item was disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Disposes all the resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
