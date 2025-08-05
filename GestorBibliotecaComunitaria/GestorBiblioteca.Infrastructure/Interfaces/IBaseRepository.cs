namespace GestorBiblioteca.Infrastructure.Interfaces
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        void Cadastrar(TEntity obj);
        void Atualizar(TEntity obj, int id);
        TEntity BuscarPorId(int id);
        IEnumerable<TEntity> BuscarTodos();     
    }
}
