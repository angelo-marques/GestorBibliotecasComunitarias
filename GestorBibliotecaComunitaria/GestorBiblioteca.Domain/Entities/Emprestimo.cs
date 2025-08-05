using GestorBiblioteca.Domain.Entities.Interfaces;
using GestorBiblioteca.Domain.Enums;
using MicroServico.Domain.Validators;

namespace GestorBiblioteca.Domain.Entities
{
    public class Emprestimo : BaseEntity, IEntityValidate
    {
        protected Emprestimo() { }

        public Emprestimo(int livroId, Livro livro, DateTime dataEmprestimo, DateTime dataDevolucao, EmprestimoStatusEnum status)
        {
            LivroId = livroId;
            Livro = livro;
            DataEmprestimo = dataEmprestimo;
            DataDevolucao = dataDevolucao;
            Status = status;
            Validate();
        }

        public int LivroId { get; private set; }
        public Livro Livro { get; private set; } 
        public DateTime DataEmprestimo { get; private set; }
        public DateTime DataDevolucao { get; private set; }
        public EmprestimoStatusEnum Status { get; private set; }

        public void DevolverLivro()
        {
            if (Status == EmprestimoStatusEnum.Devolvido)
                throw new ArgumentException("Empréstimo já devolvido");
            Status = EmprestimoStatusEnum.Devolvido;
            DataDevolucao = DateTime.UtcNow;
            Validate();
        }

        public bool Validate()
        {
            AssertionConcern.ValidaStatusRange((int)Status, "Status invalido.");
            AssertionConcern.ValidaDataMaiorOuIgual(DataEmprestimo, DataDevolucao, "Data de emprestimo esta maior que a data de devolução");
            AssertionConcern.ValidaObjetoNaoNulo(Livro, "Sem livro.");
            return true;
        }
    }
}
