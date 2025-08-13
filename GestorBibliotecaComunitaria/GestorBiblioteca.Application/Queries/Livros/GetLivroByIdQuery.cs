using GestorBiblioteca.Application.ViewModels;
using GestorBiblioteca.Infrastructure.Events;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Queries.Livros
{
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
        private readonly ILivroMongoRepository _livroMongoRepository;
        public GetLivroByIdQueryHandler(IBaseMongoContext context)
        {
            _livroMongoRepository = new LivroMongoEventsRepository(context, erpName: "Livro");
        }
        public async Task<ResultViewModel<LivroViewModel>> Handle(GetLivroByIdQuery request, CancellationToken cancellationToken)
        {
            var livro = _livroMongoRepository.BuscarPorId(request.Id);

            if (livro == null)            
                return ResultViewModel<LivroViewModel>.Error("Livro não encontrado.");            

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