using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Infrastructure.Interfaces;
using GestorBiblioteca.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GestorBiblioteca.Infrastructure.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private GestorBibliotecaDbContext _context;
        public EmprestimoRepository(GestorBibliotecaDbContext dbContext) 
        {
            _context = dbContext;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Atualizar(Emprestimo obj)
        {
            _context.Emprestimos.Update(obj);
        }

        public async Task<Emprestimo> BuscarPorId(int id)
        {
            return await _context.Emprestimos.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Emprestimo>> BuscarTodos()
        {
            return await _context.Emprestimos.AsNoTracking().ToListAsync();
        }
        

        public void Cadastrar(Emprestimo obj)
        {
            _context.Emprestimos.Add(obj);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
