using GestorBiblioteca.Domain.Entities;

namespace GestorBiblioteca.Infrastructure.Interfaces
{
    public interface ILivroMongoRepository : IBaseRepository<Livro>
    {
        bool VerificaSeExisteLivro(int codigo);
    }
}
