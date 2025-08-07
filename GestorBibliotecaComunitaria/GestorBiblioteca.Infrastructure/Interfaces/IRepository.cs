namespace GestorBiblioteca.Infrastructure.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class 
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
