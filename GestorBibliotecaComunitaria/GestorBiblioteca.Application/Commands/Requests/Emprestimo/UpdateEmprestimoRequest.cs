using GestorBiblioteca.Application.Commands.Interfaces;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Domain.Enums;
using MediatR;

namespace GestorBiblioteca.Application.Commands.Requests.Emprestimo
{
    public class UpdateEmprestimoRequest : IRequest<GenericCommandResponse>
    {
        public UpdateEmprestimoRequest(int id, DateTime createdAt, bool isDeleted, int livroId, Livro livro, DateTime dataEmprestimo, DateTime dataDevolucao, EmprestimoStatusEnum status)
        {
            Id = id;
            CreatedAt = createdAt;
            IsDeleted = isDeleted;
            LivroId = livroId;
            Livro = livro;
            DataEmprestimo = dataEmprestimo;
            DataDevolucao = dataDevolucao;
            Status = status;
        }

        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public int LivroId { get; private set; }
        public Livro Livro { get; private set; }
        public DateTime DataEmprestimo { get; private set; }
        public DateTime DataDevolucao { get; private set; }
        public EmprestimoStatusEnum Status { get; private set; }

       
    }
}
