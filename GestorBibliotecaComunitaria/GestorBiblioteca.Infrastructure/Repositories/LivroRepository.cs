using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Infrastructure.Interfaces;
using GestorBiblioteca.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GestorBiblioteca.Infrastructure.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly GestorBibliotecaDbContext _context;

        public LivroRepository(GestorBibliotecaDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
        public void Atualizar(Livro obj)
        {
            _context.Livros.Update(obj);
        }

        public async Task<Livro> BuscarPorId(int id)
        {
            return await _context.Livros.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Livro>> BuscarTodos()
        {
            return await _context.Livros.AsNoTracking().ToListAsync();
        }

        public void Cadastrar(Livro obj)
        {
            _context.Livros.Add(obj);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
