using GestorBiblioteca.Application.Commands.Requests.Livros;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Handlers.Livro
{
    public class CreateLivroHandler : IRequestHandler<CreateLivroRequest, GenericCommandResponse>
       
    {
        private readonly ILivroRepository _livroRepository;
        public CreateLivroHandler(ILivroRepository livroRepository) {
            _livroRepository = livroRepository;
        }

        public async Task<GenericCommandResponse> Handle(CreateLivroRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Livro livro = new(request.Titulo, request.Autor, request.AnoPublicacao, request.QuantidadeDisponivel);

            _livroRepository.Cadastrar(livro);
            if (_livroRepository.UnitOfWork.Commit().Result != true)
            {
                return new GenericCommandResponse(false, "Erro ao tentar salvar dados", null);
            }
            return new GenericCommandResponse(true, "Sucesso", livro);
        }
    }
}
