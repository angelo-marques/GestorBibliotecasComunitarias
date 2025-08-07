using GestorBiblioteca.Application.Commands.Requests.Emprestimo;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Domain.Enums;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Handlers.Emprestimo
{
    public class ReturnEmprestimoHandler : IRequestHandler<ReturnEmprestimoRequest, GenericCommandResponse>
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        public ReturnEmprestimoHandler(IEmprestimoRepository emprestimoRepository)
        {
            _emprestimoRepository = emprestimoRepository;
        }
        public async Task<GenericCommandResponse> Handle(ReturnEmprestimoRequest request, CancellationToken cancellationToken)
        {
            // Retrieve the loan from the write model
            var emprestimo = await _emprestimoRepository.BuscarPorId(request.EmprestimoId);
            if (emprestimo == null)
                return new GenericCommandResponse(false, "Empréstimo não encontrado.", null);

            // If it's already returned we can't process it again
            if (emprestimo.Status == EmprestimoStatusEnum.Devolvido)
                return new GenericCommandResponse(false, "Empréstimo já devolvido.", null);

            // Increment the quantity of the associated book and commit
            emprestimo.Livro.AumentarQuantidade();
            emprestimo.DevolverLivro();

            _emprestimoRepository.Atualizar(emprestimo);

            var commitResult = await _emprestimoRepository.UnitOfWork.Commit();
            if (!commitResult)
            {
                return new GenericCommandResponse(false, "Erro ao tentar atualizar dados.", null);
            }

            return new GenericCommandResponse(true, "Empréstimo devolvido com sucesso.", emprestimo);
        }
    }
}
