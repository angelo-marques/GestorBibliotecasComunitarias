using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Domain.Enums;
using MediatR;

namespace GestorBiblioteca.Application.Commands.Requests.Emprestimo
{
    public class CreateEmprestimoRequest : IRequest<GenericCommandResponse>
    {
        
        public CreateEmprestimoRequest(int livroId, DateTime dataEmprestimo, DateTime dataDevolucao, EmprestimoStatusEnum status)
        {
            LivroId = livroId;
            DataEmprestimo = dataEmprestimo;
            DataDevolucao = dataDevolucao;
            Status = status;
        }

        public int LivroId { get; private set; }

        public DateTime DataEmprestimo { get; private set; }
        public DateTime DataDevolucao { get; private set; }
        public EmprestimoStatusEnum Status { get; private set; }

    }
}
