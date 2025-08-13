using GestorBiblioteca.Infrastructure.Interfaces;
using GestorBiblioteca.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GestorBiblioteca.Infrastructure.Persistence
{
    public class BaseMongoDbContext : IBaseMongoContext
    {

        private IMongoDatabase IMongoDatabase { get; set; }
        private readonly List<Func<Task>> _commands;

        public BaseMongoDbContext(IOptions<MongoSettings> configuration)
        {
            MongoClient mongoClient = new(configuration.Value.ConnectionString);
            IMongoDatabase = mongoClient.GetDatabase(configuration.Value.DatabaseName);
            _commands = new();
        }

        public IMongoCollection<T> GetCollection<T>(string? name)
        {
            return IMongoDatabase.GetCollection<T>(name);
        }

        public int SaveChanges()
        {
            int quantidade = _commands.Count;
            for (int i = 0; i < quantidade; i++)
            {
                Func<Task> command = _commands[i];
                command();
            }
            _commands.Clear();
            return quantidade;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task AddCommand(Func<Task> func)
        {
            _commands.Add(func);
            return Task.CompletedTask;
        }

        
    }
}
