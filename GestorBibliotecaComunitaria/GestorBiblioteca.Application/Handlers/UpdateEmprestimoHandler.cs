using GestorBiblioteca.Application.Commands.Requests.Emprestimo;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Handlers
{
    public class UpdateEmprestimoHandler : IRequestHandler<UpdateEmprestimoRequest, GenericCommandResponse>
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroRepository _livroRepository;
        public UpdateEmprestimoHandler(IEmprestimoRepository emprestimoRepository, ILivroRepository livroRepository) {

            _livroRepository = livroRepository;
            _emprestimoRepository = emprestimoRepository;
        }
    
    
        public Task<GenericCommandResponse> Handle(UpdateEmprestimoRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
