using GestorBiblioteca.Application.Commands.Requests.Livros;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Infrastructure.Events;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Handlers.Livro
{
    public class CreateLivroHandler : IRequestHandler<CreateLivroRequest, GenericCommandResponse>       
    {
        private readonly ILivroRepository _livroRepository;
        private readonly ILivroMongoRepository _livroMongoRepository;
        private readonly IUnitOfWork _unitOfWork;        

        public CreateLivroHandler(ILivroRepository livroRepository, IBaseMongoContext context, IUnitOfWork unitOfWork) {
            _livroRepository = livroRepository;
            _livroMongoRepository = new LivroMongoEventsRepository(context, erpName: "Livro");
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericCommandResponse> Handle(CreateLivroRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Domain.Entities.Livro livro = new(request.Titulo, request.Autor, request.AnoPublicacao, request.QuantidadeDisponivel, request.QuantidadeDisponivel);
                
                _livroRepository.Cadastrar(livro);
                
                if (!_livroRepository.UnitOfWork.Commit().Result)
                    return new GenericCommandResponse(false, "Erro ao tentar salvar dados no banco relaciona.", null);

                _livroMongoRepository.Cadastrar(livro);
                
                if (!_unitOfWork.Commit().Result)
                    return new GenericCommandResponse(false, "Erro ao tentar salvar dados no banco documental.", null);

                return new GenericCommandResponse(true, "Sucesso", livro);
            }
            catch (Exception exception)
            {
                return new GenericCommandResponse(false, exception.Message, null);
            }
        }
    }
}
