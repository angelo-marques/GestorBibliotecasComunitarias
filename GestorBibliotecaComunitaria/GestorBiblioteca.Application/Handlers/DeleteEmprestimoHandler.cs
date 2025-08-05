using GestorBiblioteca.Application.Commands.Requests.Emprestimo;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Handlers
{
    public class DeleteEmprestimoHandler :IRequestHandler<DeleteEmprestimoRequest, GenericCommandResponse>
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroRepository _livroRepository;
        public DeleteEmprestimoHandler(IEmprestimoRepository emprestimoRepository, ILivroRepository livroRepository) {

            _livroRepository = livroRepository;
            _emprestimoRepository = emprestimoRepository;
        }
    
        public async Task<GenericCommandResponse> Handle(CreateEmprestimoRequest request, CancellationToken cancellationToken)
        {
            if (request.LivroId <= 0)
                return new GenericCommandResponse(false, "Identificador do livro invalido.", null);

            var livro = await _livroRepository.BuscarPorId(request.LivroId);

           if (livro.QuantidadeDisponivel <= 0)
                return new GenericCommandResponse(false, "Não tem livro no estoque.", null);

            if (request.Livro == null)
                return new GenericCommandResponse(false, "Livro não pode ser vazio", null);

            Emprestimo emprestimo = new(request.LivroId, livro, request.DataEmprestimo, request.DataDevolucao, request.Status);

            _emprestimoRepository.Cadastrar(emprestimo);
            if (_emprestimoRepository.UnitOfWork.Commit().Result != true)
            {
                return new GenericCommandResponse(false, "Erro ao tentar salvar dados", null);
            }
            
            return new GenericCommandResponse(true, "Livro Cadastrado com sucesso", emprestimo);
        }

        public Task<GenericCommandResponse> Handle(DeleteEmprestimoRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<GenericCommandResponse> Handle(UpdateEmprestimoRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
