using GestorBiblioteca.Application.Commands.Responses;
using MediatR;

namespace GestorBiblioteca.Application.Commands.Requests.Emprestimo
{
    public class ReturnEmprestimoRequest : IRequest<GenericCommandResponse>
    {
        public ReturnEmprestimoRequest(int emprestimoId)
        {
            EmprestimoId = emprestimoId;
        }

        public int EmprestimoId { get; }
    }
}
