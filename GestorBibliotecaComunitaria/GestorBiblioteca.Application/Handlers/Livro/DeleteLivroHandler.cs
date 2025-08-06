using GestorBiblioteca.Application.Commands.Requests.Livros;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Handlers.Livro
{
    public class DeleteLivroHandler :  IRequestHandler<DeleteLivroRequest, GenericCommandResponse>
     
    {
        private readonly ILivroRepository _livroRepository;
        public DeleteLivroHandler(ILivroRepository livroRepository) {
            _livroRepository = livroRepository;
        }

        public Task<GenericCommandResponse> Handle(DeleteLivroRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GenericCommandResponse(false, "Exclusão de livros não suportada.", null));
        }


    }
}
