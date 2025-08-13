using GestorBiblioteca.Application.ViewModels;
using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Infrastructure.Events;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Queries.Livros
{
    public class GetAllLivrosQuery : IRequest<ResultViewModel<List<LivroViewModel>>>
    {
        public GetAllLivrosQuery(string? search = null)
        {
            Search = search;
        }

        public string? Search { get; }
    }

    public class GetAllLivrosQueryHandler : IRequestHandler<GetAllLivrosQuery, ResultViewModel<List<LivroViewModel>>>
    {
        private readonly ILivroMongoRepository _livroMongoRepository;
        public GetAllLivrosQueryHandler(IBaseMongoContext context)
        {
            _livroMongoRepository = new LivroMongoEventsRepository(context, erpName: "Livro");
        }
        public async Task<ResultViewModel<List<LivroViewModel>>> Handle(GetAllLivrosQuery request, CancellationToken cancellationToken)
        {
            var livros = _livroMongoRepository.BuscarTodos().ToList().Select(l => new LivroViewModel
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Autor = l.Autor,
                AnoPublicacao = l.AnoPublicacao,
                QuantidadeDisponivel = l.QuantidadeDisponivel,
                QuantidadeCadastrada = (int)l.QuantidadeCadastrada == null ? 0 : (int)l.QuantidadeCadastrada
            });

            if (!livros.Any())
            {
                return ResultViewModel<List<LivroViewModel>>.Error("Sem dados.");
            }
           
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                string search = request.Search.ToLower();
                livros = livros.Where(l => l.Titulo.ToLower().Contains(search) || l.Autor.ToLower().Contains(search));
            }
            return ResultViewModel<List<LivroViewModel>>.Success(livros.ToList());
        }
    }
}