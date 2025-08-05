using GestorBiblioteca.Domain.Entities;

namespace GestorBiblioteca.Infrastructure.Interfaces
{
    public interface ILivroRepository : IRepository<Livro>
    {
        void Cadastrar(Livro obj);
        void Atualizar(Livro obj);
        Task<Livro> BuscarPorId(int id);
        Task<IEnumerable<Livro>> BuscarTodos();
    }
}
