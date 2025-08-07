using GestorBiblioteca.Application.Commands.Requests.Emprestimo;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Handlers.Emprestimo
{
    public class UpdateEmprestimoHandler : IRequestHandler<UpdateEmprestimoRequest, GenericCommandResponse>
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroRepository _livroRepository;
        public UpdateEmprestimoHandler(IEmprestimoRepository emprestimoRepository, ILivroRepository livroRepository)
        {

            _livroRepository = livroRepository;
            _emprestimoRepository = emprestimoRepository;
        }
        public Task<GenericCommandResponse> Handle(UpdateEmprestimoRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GenericCommandResponse(false, "Atualização de empréstimo não suportada.", null));
        }
    }
}
