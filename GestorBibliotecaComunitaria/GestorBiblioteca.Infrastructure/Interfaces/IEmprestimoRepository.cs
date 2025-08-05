using GestorBiblioteca.Domain.Entities;

namespace GestorBiblioteca.Infrastructure.Interfaces
{
    public interface IEmprestimoRepository : IRepository<Emprestimo>
    {
        void Cadastrar(Emprestimo obj);
        void Atualizar(Emprestimo obj);
        Task<Emprestimo> BuscarPorId(int id);
        Task<IEnumerable<Emprestimo>> BuscarTodos();
    }
}
