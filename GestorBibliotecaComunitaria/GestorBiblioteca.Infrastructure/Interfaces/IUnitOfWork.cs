namespace GestorBiblioteca.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}