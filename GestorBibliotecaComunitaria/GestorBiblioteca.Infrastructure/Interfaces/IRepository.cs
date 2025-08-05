using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBiblioteca.Infrastructure.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class 
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
