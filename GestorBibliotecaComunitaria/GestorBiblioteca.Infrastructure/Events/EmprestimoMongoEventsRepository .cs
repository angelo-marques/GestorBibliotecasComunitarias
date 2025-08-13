using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Infrastructure.Interfaces;
using GestorBiblioteca.Infrastructure.Repositories;
using MongoDB.Driver;

namespace GestorBiblioteca.Infrastructure.Events
{
    public class EmprestimoMongoEventsRepository : BaseMongoRepository<Emprestimo>, IEmprestimoMongoRepository
    {
        public EmprestimoMongoEventsRepository(IBaseMongoContext context, string erpName = "Emprestimo") : base(context, erpName)
        {
        }
        

        public bool VerificaSeExisteLivro(int codigo)
        {
            Emprestimo result = _dbCollection.Find(Builders<Emprestimo>.Filter.Eq("_id", codigo)).FirstOrDefault();
            return result != null;
        }
    }
}
