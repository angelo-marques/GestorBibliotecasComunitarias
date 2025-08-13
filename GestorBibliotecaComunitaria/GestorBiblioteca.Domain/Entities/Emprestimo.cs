using GestorBiblioteca.Domain.Entities.Interfaces;
using GestorBiblioteca.Domain.Enums;
using MicroServico.Domain.Validators;
using MongoDB.Bson.Serialization.Attributes;

namespace GestorBiblioteca.Domain.Entities
{
    public class Emprestimo : BaseEntity, IEntityValidate
    {
        protected Emprestimo() { }

        public Emprestimo(int livroId,  DateTime dataEmprestimo, DateTime dataDevolucao, EmprestimoStatusEnum status)
        {
            LivroId = livroId;
            DataEmprestimo = dataEmprestimo;
            DataDevolucao = dataDevolucao;
            Status = status;
            Validate();
        }
        [BsonElement("livro_id")]
        public int LivroId { get; private set; }
        [BsonIgnore]
        public virtual Livro Livro { get; private set; }
        [BsonElement("data_emprestimo")]
        public DateTime DataEmprestimo { get; private set; }
        [BsonElement("data_devolucao")]
        public DateTime DataDevolucao { get; private set; }
        [BsonElement("status")]
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
            AssertionConcern.ValidaValorMaiorQueZero((int)LivroId, "Livro não pode ser vazio.");
            AssertionConcern.ValidaStatusRange((int)Status, "Status invalido.");
            AssertionConcern.ValidaDataMaiorOuIgual(DataEmprestimo, DataDevolucao, "Data de emprestimo esta maior que a data de devolução");           
            return true;
        }
    }
}
