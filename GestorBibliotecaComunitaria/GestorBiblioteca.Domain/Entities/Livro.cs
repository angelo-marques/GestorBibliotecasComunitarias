using GestorBiblioteca.Domain.Entities.Interfaces;
using MicroServico.Domain.Validators;

namespace GestorBiblioteca.Domain.Entities
{
    public class Livro : BaseEntity, IEntityValidate
    {
        protected Livro() { }

        public Livro(string titulo, string autor, int anoPublicacao, int quantidadeDisponivel)
        {
            Titulo = titulo;
            Autor = autor; 
            AnoPublicacao = anoPublicacao;
            QuantidadeDisponivel = quantidadeDisponivel;
            Validate();
        }

        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public int AnoPublicacao { get; private set; }
        public int QuantidadeDisponivel { get; private set; }
        public int IdEmprestimo { get; private set; }
        public List<Emprestimo> Emprestimos { get; private set; }


        public void Update(string autor, string titulo, int quantidadeDisponivel, int anoPublicacao)
        {
            DiminuirQuantidade();
            Autor = autor; 
            Titulo = titulo; 
            QuantidadeDisponivel = quantidadeDisponivel;
            AnoPublicacao = anoPublicacao;
            Validate();
        }

        public void DiminuirQuantidade()
        {
            if (QuantidadeDisponivel <= 0)
                throw new ArgumentException("Livro indisponível para empréstimo");
            QuantidadeDisponivel--;
        }

        public void AumentarQuantidade()
        {
            QuantidadeDisponivel++;
        }

        public bool Validate()
        {
            AssertionConcern.ValidaStringNaoVaziaOuNula(Autor, "Autor é obrigatório.");
            AssertionConcern.ValidaStringNaoVaziaOuNula(Titulo, "Título é obrigatório.");
            AssertionConcern.ValidaValorMaiorQueZero(QuantidadeDisponivel, "Quantidade disponível não pode ser zero ou negativa.");
            return true;
        }
    }
}
