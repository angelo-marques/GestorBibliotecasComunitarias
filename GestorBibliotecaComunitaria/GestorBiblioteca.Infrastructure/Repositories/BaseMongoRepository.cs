using GestorBiblioteca.Infrastructure.Interfaces;
using MongoDB.Driver;

namespace GestorBiblioteca.Infrastructure.Repositories
{
    public abstract class BaseMongoRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly IBaseMongoContext _context;
        protected IMongoCollection<TEntity> _dbCollection;
        protected  BaseMongoRepository(IBaseMongoContext context, string collectionName)
        {
            _context = context;
            _dbCollection = _context.GetCollection<TEntity>(collectionName);
        }
        public virtual IEnumerable<TEntity> BuscarTodos()
        {
            IFindFluent<TEntity, TEntity> all = _dbCollection.Find(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public virtual TEntity? BuscarPorId(int id)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("codigo_id", id);
            TEntity? resposta = _dbCollection.Find(filter).FirstOrDefault();
            return resposta;
        }

        public virtual void Cadastrar(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"{nameof(Cadastrar)} {typeof(TEntity).Name} não deve ser nulo");
            }
             _context.AddCommand(() => _dbCollection.InsertOneAsync(obj));
        }

        public virtual void Atualizar(TEntity obj, int id)
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"{nameof(Atualizar)} {typeof(TEntity).Name} não deve ser nulo");
            }

            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("codigo_id", id);

            _context.AddCommand(() => _dbCollection.ReplaceOneAsync(filter, obj));
            _context.AddCommand(() => _dbCollection.UpdateOneAsync(filter, Builders<TEntity>.Update.Set("data_alteracao", DateTime.Now.AddHours(-3))));
        }

        public virtual void DeletarPorId(int id)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("codigo_id", id);

            _context.AddCommand(() => _dbCollection.DeleteOneAsync(filter));
        }
     
        public void Dispose()
        {
            _context?.Dispose();
        }
    }

}


