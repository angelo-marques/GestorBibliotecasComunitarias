using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Infrastructure.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace GestorBiblioteca.Tests.Fakes
{
 
    public class FakeUnitOfWork : IUnitOfWork
    {
        public void Dispose()
        {
        }

        public Task<bool> Commit()
        {
            return Task.FromResult(true);
        }
    }

    public class FakeLivroRepository : ILivroRepository
    {
        private readonly ConcurrentDictionary<int, Livro> _storage = new();
        private int _currentId = 1;
        public IUnitOfWork UnitOfWork { get; } = new FakeUnitOfWork();

        public void Cadastrar(Livro obj)
        {
            var id = Interlocked.Increment(ref _currentId);
            var prop = typeof(Livro).GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            prop!.SetValue(obj, id);
            _storage[id] = obj;
        }

        public void Atualizar(Livro obj)
        {
            // In this fake repository entities are mutable, so no action is required
        }

        public Task<Livro> BuscarPorId(int id)
        {
            _storage.TryGetValue(id, out var livro);
            return Task.FromResult(livro!);
        }

        public Task<IEnumerable<Livro>> BuscarTodos()
        {
            return Task.FromResult(_storage.Values.AsEnumerable());
        }

        public void Dispose()
        {
        }
    }

    /// <summary>
    /// In-memory repository used for testing the Emprestimo aggregate.  Loans reference Livro objects held by the
    /// <see cref="FakeLivroRepository"/>.  Identifiers are assigned automatically via reflection.
    /// </summary>
    public class FakeEmprestimoRepository : IEmprestimoRepository
    {
        private readonly ConcurrentDictionary<int, Emprestimo> _storage = new();
        private int _currentId = 1;
        public IUnitOfWork UnitOfWork { get; } = new FakeUnitOfWork();

        public void Cadastrar(Emprestimo obj)
        {
            var id = Interlocked.Increment(ref _currentId);
            var prop = typeof(Emprestimo).GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            prop!.SetValue(obj, id);
            _storage[id] = obj;
        }

        public void Atualizar(Emprestimo obj)
        {
            // Entities are stored by reference; nothing needed here
        }

        public Task<Emprestimo> BuscarPorId(int id)
        {
            _storage.TryGetValue(id, out var emprestimo);
            return Task.FromResult(emprestimo!);
        }

        public Task<IEnumerable<Emprestimo>> BuscarTodos()
        {
            return Task.FromResult(_storage.Values.AsEnumerable());
        }

        public void Dispose()
        {
        }
    }
}