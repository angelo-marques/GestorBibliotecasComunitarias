using GestorBiblioteca.Application.ViewModels;
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
        private readonly ILivroRepository _livroRepository;
        public GetAllLivrosQueryHandler(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }
        public async Task<ResultViewModel<List<LivroViewModel>>> Handle(GetAllLivrosQuery request, CancellationToken cancellationToken)
        {
            var livros = await _livroRepository.BuscarTodos();
            var list = livros.Select(l => new LivroViewModel
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Autor = l.Autor,
                AnoPublicacao = l.AnoPublicacao,
                QuantidadeDisponivel = l.QuantidadeDisponivel,
                QuantidadeCadastrada = (int)l.QuantidadeCadastrada
            });
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                string search = request.Search.ToLower();
                list = list.Where(l => l.Titulo.ToLower().Contains(search) || l.Autor.ToLower().Contains(search));
            }
            return ResultViewModel<List<LivroViewModel>>.Success(list.ToList());
        }
    }
}