using GestorBiblioteca.Application.Commands.Requests.Emprestimo;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Domain.Enums;
using GestorBiblioteca.Infrastructure.Events;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Handlers.Emprestimo
{
    public class ReturnEmprestimoHandler : IRequestHandler<ReturnEmprestimoRequest, GenericCommandResponse>
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly IEmprestimoMongoRepository _emprestimoMongoRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ReturnEmprestimoHandler(IEmprestimoRepository emprestimoRepository, IBaseMongoContext context)
        {
            _emprestimoRepository = emprestimoRepository;
            _emprestimoMongoRepository = new EmprestimoMongoEventsRepository(context, erpName: "Emprestimo");
        }

        public IUnitOfWork UnitOfWork => _unitOfWork;

        public async Task<GenericCommandResponse> Handle(ReturnEmprestimoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var emprestimo = await _emprestimoRepository.BuscarPorId(request.EmprestimoId);
                if (emprestimo == null)
                    return new GenericCommandResponse(false, "Empréstimo não encontrado.", null);

                if (emprestimo.Status == EmprestimoStatusEnum.Devolvido)
                    return new GenericCommandResponse(false, "Empréstimo já devolvido.", null);

                emprestimo.Livro.AumentarQuantidade();
                emprestimo.DevolverLivro();
                _emprestimoRepository.Atualizar(emprestimo);

                if (!await _emprestimoRepository.UnitOfWork.Commit())
                    return new GenericCommandResponse(false, "Erro ao tentar atualizar dados.", null);

                _emprestimoMongoRepository.Atualizar(emprestimo, emprestimo.Id);

                if (!await UnitOfWork.Commit())
                    return new GenericCommandResponse(false, "Erro ao tentar atualizar dados.", null);

                return new GenericCommandResponse(true, "Empréstimo devolvido com sucesso.", emprestimo);
            }
            catch (Exception ex)
            {
                return new GenericCommandResponse(false, ex.Message, null);
            }
        }
    }
}
