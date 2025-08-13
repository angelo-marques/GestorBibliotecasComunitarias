using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Infrastructure.Interfaces;
using GestorBiblioteca.Infrastructure.Repositories;
using MongoDB.Driver;

namespace GestorBiblioteca.Infrastructure.Events
{
    public class LivroMongoEventsRepository : BaseMongoRepository<Livro>, ILivroMongoRepository
    {
        public LivroMongoEventsRepository(IBaseMongoContext context, string erpName = "Livro") : base(context, erpName)
        {
        }
        

        public bool VerificaSeExisteLivro(int codigo)
        {
            Livro result = _dbCollection.Find(Builders<Livro>.Filter.Eq("codigo_id", codigo)).FirstOrDefault();
            return result != null;
        }
    }
}
