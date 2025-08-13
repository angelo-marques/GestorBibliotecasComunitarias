using GestorBiblioteca.Application.ViewModels;
using GestorBiblioteca.Domain.Enums;
using GestorBiblioteca.Infrastructure.Events;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Queries.Emprestimos
{
    public class GetEmprestimoByIdQuery : IRequest<ResultViewModel<EmprestimoViewModel>>
    {
        public GetEmprestimoByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class GetEmprestimoByIdQueryHandler : IRequestHandler<GetEmprestimoByIdQuery, ResultViewModel<EmprestimoViewModel>>
    {
        private readonly IEmprestimoMongoRepository _emprestimoMongoRepository;
        public GetEmprestimoByIdQueryHandler(IBaseMongoContext context)
        {
            _emprestimoMongoRepository = new EmprestimoMongoEventsRepository(context, erpName: "Emprestimo");
        }
        public async Task<ResultViewModel<EmprestimoViewModel>> Handle(GetEmprestimoByIdQuery request, CancellationToken cancellationToken)
        {
            var emprestimo = _emprestimoMongoRepository.BuscarPorId(request.Id);
            
            if (emprestimo == null)
                return ResultViewModel<EmprestimoViewModel>.Error("Empréstimo não encontrado.");

            var viewModel = new EmprestimoViewModel
            {
                Id = emprestimo.Id,
                LivroId = emprestimo.LivroId,
                TituloLivro = emprestimo.Livro?.Titulo ?? string.Empty,
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataDevolucao = emprestimo.Status == EmprestimoStatusEnum.Devolvido ? emprestimo.DataDevolucao : null,
                Status = emprestimo.Status.ToString()
            };

            return ResultViewModel<EmprestimoViewModel>.Success(viewModel);
        }
    }
}