using GestorBiblioteca.Application.Commands.Requests.Emprestimo;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Infrastructure.Events;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Handlers.Emprestimo
{
    public class CreateEmprestimoHandler : IRequestHandler<CreateEmprestimoRequest, GenericCommandResponse>
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroRepository _livroRepository;
        private readonly IEmprestimoMongoRepository _emprestimoMongoRepository;
        private readonly ILivroMongoRepository _livroMongoRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateEmprestimoHandler(IEmprestimoRepository emprestimoRepository, ILivroRepository livroRepository, IBaseMongoContext context, IUnitOfWork unitOfWork) {

            _livroRepository = livroRepository;
            _emprestimoRepository = emprestimoRepository;
            _emprestimoMongoRepository = new EmprestimoMongoEventsRepository(context, erpName: "Emprestimo");
            _livroMongoRepository = new LivroMongoEventsRepository(context, erpName: "Livro");
            _unitOfWork = unitOfWork;
        }
        public async Task<GenericCommandResponse> Handle(CreateEmprestimoRequest request, CancellationToken cancellationToken)
        {          
            try
            {
                if (request.LivroId <= 0)
                    return new GenericCommandResponse(false, "Identificador do livro invalido.", null);

                var livro = await _livroRepository.BuscarPorId(request.LivroId);

                if (livro.QuantidadeDisponivel <= 0)
                    return new GenericCommandResponse(false, "Não há exemplares disponíveis em estoque.", null);

                livro.DiminuirQuantidade();

                Domain.Entities.Emprestimo emprestimo = new(request.LivroId, request.DataEmprestimo, request.DataDevolucao, request.Status);

                _livroRepository.Atualizar(livro);
                _emprestimoRepository.Cadastrar(emprestimo);

                if (!await _emprestimoRepository.UnitOfWork.Commit())
                    return new GenericCommandResponse(false, "Erro ao tentar salvar dados do empréstimo no banco relaciona.", null);

                _livroMongoRepository.Atualizar(livro, livro.Id);
                _emprestimoMongoRepository.Cadastrar(emprestimo);

                if (!await _unitOfWork.Commit())
                    return new GenericCommandResponse(false, "Erro ao tentar salvar dados do empréstimo no banco documental.", null);

                return new GenericCommandResponse(true, "Empréstimo criado com sucesso.", emprestimo);
            }
            catch (Exception ex)
            {
                return new GenericCommandResponse(false, ex.Message, null);
            }           
        }
    }
}
