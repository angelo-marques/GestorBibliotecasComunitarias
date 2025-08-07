using GestorBiblioteca.Application.Commands.Requests.Emprestimo;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Handlers.Emprestimo
{
    public class CreateEmprestimoHandler : IRequestHandler<CreateEmprestimoRequest, GenericCommandResponse>
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroRepository _livroRepository;
        public CreateEmprestimoHandler(IEmprestimoRepository emprestimoRepository, ILivroRepository livroRepository) {

            _livroRepository = livroRepository;
            _emprestimoRepository = emprestimoRepository;
        }
        public async Task<GenericCommandResponse> Handle(CreateEmprestimoRequest request, CancellationToken cancellationToken)
        {
            if (request.LivroId <= 0)
                return new GenericCommandResponse(false, "Identificador do livro invalido.", null);

            if (request.Livro == null)
                return new GenericCommandResponse(false, "Livro não pode ser vazio", null);

            var livro = await _livroRepository.BuscarPorId(request.LivroId);

            if (livro.QuantidadeDisponivel <= 0)
                return new GenericCommandResponse(false, "Não há exemplares disponíveis em estoque.", null);

            try
            {
                livro.DiminuirQuantidade();
            }
            catch (Exception ex)
            {
                return new GenericCommandResponse(false, ex.Message, null);
            }

            Domain.Entities.Emprestimo emprestimo = new(request.LivroId, livro, request.DataEmprestimo, request.DataDevolucao, request.Status);

            _livroRepository.Atualizar(livro);
            _emprestimoRepository.Cadastrar(emprestimo);
            var commitResult = await _emprestimoRepository.UnitOfWork.Commit();
            if (!commitResult)
            {
                return new GenericCommandResponse(false, "Erro ao tentar salvar dados.", null);
            }

            return new GenericCommandResponse(true, "Empréstimo criado com sucesso.", emprestimo);
        }
    }
}
