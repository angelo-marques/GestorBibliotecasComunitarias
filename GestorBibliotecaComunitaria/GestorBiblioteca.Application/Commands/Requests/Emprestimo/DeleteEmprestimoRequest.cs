using GestorBiblioteca.Application.Commands.Interfaces;
using GestorBiblioteca.Application.Commands.Responses;
using MediatR;

namespace GestorBiblioteca.Application.Commands.Requests.Emprestimo
{
    public class DeleteEmprestimoRequest : IRequest<GenericCommandResponse>
    {
        public DeleteEmprestimoRequest(int id, bool isDeleted)
        {
            Id = id;
            IsDeleted = isDeleted;
        }

        public int Id { get; private set; }
        public bool IsDeleted { get; private set; }
     
    }
}
