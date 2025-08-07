using GestorBiblioteca.Application.Commands.Requests.Livros;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Handlers.Livro
{
    public class UpdateLivroHandler : IRequestHandler<UpdateLivroRequest, GenericCommandResponse>
    {
        private readonly ILivroRepository _livroRepository;
        public UpdateLivroHandler(ILivroRepository livroRepository) {
            _livroRepository = livroRepository;
        }

        public async Task<GenericCommandResponse> Handle(UpdateLivroRequest request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                return new GenericCommandResponse(false, "Identificador do livro inválido.", null);

            var livro = await _livroRepository.BuscarPorId(request.Id);
            if (livro == null)
                return new GenericCommandResponse(false, "Livro não encontrado.", null);

            try
            {
                livro.Update(request.Autor, request.Titulo, request.QuantidadeDisponivel, request.AnoPublicacao);
            }
            catch (Exception ex)
            {
                return new GenericCommandResponse(false, ex.Message, null);
            }

            _livroRepository.Atualizar(livro);
            var commitResult = await _livroRepository.UnitOfWork.Commit();
            if (!commitResult)
            {
                return new GenericCommandResponse(false, "Erro ao atualizar o livro.", null);
            }
            return new GenericCommandResponse(true, "Livro atualizado com sucesso.", livro);
        }
    }
}
