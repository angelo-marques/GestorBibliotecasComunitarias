using GestorBiblioteca.Application.Commands.Responses;
using MediatR;


namespace GestorBiblioteca.Application.Commands.Requests.Livros
{
    public class CreateLivroRequest : IRequest<GenericCommandResponse>
    {
    
        public CreateLivroRequest(string titulo, string autor, int anoPublicacao, int quantidadeDisponivel)
        {
            Titulo = titulo;
            Autor = autor;
            AnoPublicacao = anoPublicacao;
            QuantidadeDisponivel = quantidadeDisponivel;
        }

        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public int AnoPublicacao { get; private set; }
        public int QuantidadeDisponivel { get; private set; }

  
    }
}
