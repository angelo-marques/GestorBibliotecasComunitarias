using GestorBiblioteca.Application.ViewModels;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Queries.Livros
{
    /// <summary>
    /// Query for retrieving a single book by its identifier.
    /// </summary>
    public class GetLivroByIdQuery : IRequest<ResultViewModel<LivroViewModel>>
    {
        public GetLivroByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class GetLivroByIdQueryHandler : IRequestHandler<GetLivroByIdQuery, ResultViewModel<LivroViewModel>>
    {
        private readonly ILivroRepository _livroRepository;
        public GetLivroByIdQueryHandler(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }
        public async Task<ResultViewModel<LivroViewModel>> Handle(GetLivroByIdQuery request, CancellationToken cancellationToken)
        {
            var livro = await _livroRepository.BuscarPorId(request.Id);
            if (livro == null)
            {
                return ResultViewModel<LivroViewModel>.Error("Livro não encontrado.");
            }
            var viewModel = new LivroViewModel
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                AnoPublicacao = livro.AnoPublicacao,
                QuantidadeDisponivel = livro.QuantidadeDisponivel
            };
            return ResultViewModel<LivroViewModel>.Success(viewModel);
        }
    }
}