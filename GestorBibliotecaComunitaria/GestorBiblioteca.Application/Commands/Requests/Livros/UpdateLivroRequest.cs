using GestorBiblioteca.Application.Commands.Interfaces;
using GestorBiblioteca.Application.Commands.Responses;
using MediatR;

namespace GestorBiblioteca.Application.Commands.Requests.Livros
{
    public class UpdateLivroRequest : IRequest<GenericCommandResponse>
    {

        public UpdateLivroRequest(int id, string titulo, string autor, int anoPublicacao, int quantidadeDisponivel, DateTime createdAt = default, bool isDeleted = false)
        {
            Titulo = titulo;
            Autor = autor;
            AnoPublicacao = anoPublicacao;
            QuantidadeDisponivel = quantidadeDisponivel;
            CreatedAt = createdAt;
            IsDeleted = isDeleted;
        }

        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public int AnoPublicacao { get; private set; }
        public int QuantidadeDisponivel { get; private set; }

       
    }
}
