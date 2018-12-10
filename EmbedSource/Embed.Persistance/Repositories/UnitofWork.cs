using Embed.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embed.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private bool disposed = false;

        public IProductRepository Products { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException($"{nameof(context)} was not supplied.");

            _context = context;                                                                                                                                                                                                          
            Products = new ProductRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
