using GestorBiblioteca.Application.Commands.Requests.Livros;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Infrastructure.Events;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Handlers.Livro
{
    public class UpdateLivroHandler : IRequestHandler<UpdateLivroRequest, GenericCommandResponse>
    {
        private readonly ILivroRepository _livroRepository;
        private readonly ILivroMongoRepository _livroMongoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLivroHandler(ILivroRepository livroRepository, IBaseMongoContext context, IUnitOfWork unitOfWork) {
            _livroRepository = livroRepository;
            _livroMongoRepository = new LivroMongoEventsRepository(context, erpName: "Livro");
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericCommandResponse> Handle(UpdateLivroRequest request, CancellationToken cancellationToken)
        {     
            try
            {
                if (request.Id <= 0)
                    return new GenericCommandResponse(false, "Identificador do livro inválido.", null);

                var livro = await _livroRepository.BuscarPorId(request.Id);

                if (livro == null)
                    return new GenericCommandResponse(false, "Livro não encontrado.", null);

                livro.Update(request.Autor, request.Titulo, request.QuantidadeDisponivel, request.AnoPublicacao);
                
                _livroRepository.Atualizar(livro);

                if (!await _livroRepository.UnitOfWork.Commit())
                    return new GenericCommandResponse(false, "Erro ao atualizar o livro no banco relacional.", null);

                _livroMongoRepository.Atualizar(livro, livro.Id);

                if (!await _unitOfWork.Commit())
                    return new GenericCommandResponse(false, "Erro ao atualizar o livro no mongo.", null);

                return new GenericCommandResponse(true, "Livro atualizado com sucesso.", livro);
            }
            catch (Exception ex)
            {
                return new GenericCommandResponse(false, ex.Message, null);
            }
        }
    }
}
