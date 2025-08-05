using GestorBiblioteca.Application.Commands.Requests.Livros;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Handlers
{
    public class UpdateLivroHandler : IRequestHandler<UpdateLivroRequest, GenericCommandResponse>
    {
        private readonly ILivroRepository _livroRepository;
        public UpdateLivroHandler(ILivroRepository livroRepository) {
            _livroRepository = livroRepository;
        }

    

        public Task<GenericCommandResponse> Handle(UpdateLivroRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
