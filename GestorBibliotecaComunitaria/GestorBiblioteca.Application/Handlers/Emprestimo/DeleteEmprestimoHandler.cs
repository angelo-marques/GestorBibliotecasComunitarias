using GestorBiblioteca.Application.Commands.Requests.Emprestimo;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Handlers.Emprestimo
{
    public class DeleteEmprestimoHandler :IRequestHandler<DeleteEmprestimoRequest, GenericCommandResponse>
    {

        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroRepository _livroRepository;
        public DeleteEmprestimoHandler(IEmprestimoRepository emprestimoRepository, ILivroRepository livroRepository)
        {

            _livroRepository = livroRepository;
            _emprestimoRepository = emprestimoRepository;
        }
        public Task<GenericCommandResponse> Handle(DeleteEmprestimoRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GenericCommandResponse(false, "A operação de exclusão de empréstimos não é suportada. Utilize a devolução.", null));
        }
    }
}
