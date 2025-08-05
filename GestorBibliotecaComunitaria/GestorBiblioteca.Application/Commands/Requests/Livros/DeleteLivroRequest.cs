using GestorBiblioteca.Application.Commands.Interfaces;
using GestorBiblioteca.Application.Commands.Responses;
using MediatR;

namespace GestorBiblioteca.Application.Commands.Requests.Livros
{
    public class DeleteLivroRequest : IRequest<GenericCommandResponse>
    {
      
        public DeleteLivroRequest(int id )
        {
            Id = id;
        }

        public int Id { get; private set; }

        public bool IsDeleted { get; private set; }


      
    }
}
