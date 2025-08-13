using GestorBiblioteca.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBiblioteca.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IBaseMongoContext _context;

        public UnitOfWork(IBaseMongoContext context)
        {
            _context = context;
        }
        public async Task<bool> Commit()
        {
            var result = _context.SaveChanges();
            return result > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
