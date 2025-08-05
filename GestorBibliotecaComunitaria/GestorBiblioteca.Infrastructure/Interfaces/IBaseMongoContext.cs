using MongoDB.Driver;

namespace GestorBiblioteca.Infrastructure.Interfaces
{
    public interface IBaseMongoContext : IDisposable
    {
        IMongoCollection<T> GetCollection<T>(string name);
        int SaveChanges();
        void Dispose();
        Task AddCommand(Func<Task> func);
    }
}


