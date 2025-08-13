using GestorBiblioteca.Domain.Entities;

namespace GestorBiblioteca.Infrastructure.Interfaces
{
    public interface IEmprestimoMongoRepository : IBaseRepository<Emprestimo>
    {
        bool VerificaSeExisteLivro(int codigo);
    }
}
