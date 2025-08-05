using GestorBiblioteca.Application.Commands.Interfaces;

namespace GestorBiblioteca.Application.Commands.Responses
{
    public record class GenericCommandResponse : ICommandResponse
    {
        public GenericCommandResponse(bool sucesso, string? menssagem, object? dados)
        {
            Sucesso = sucesso;
            Menssagem = menssagem;
            Dados = dados;
        }

        public bool Sucesso { get; set; }
        public string? Menssagem { get; set; }
        public object? Dados { get; set; }
    }
}

