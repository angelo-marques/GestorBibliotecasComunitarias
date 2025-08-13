using GestorBiblioteca.Application.ViewModels;
using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Domain.Enums;
using GestorBiblioteca.Infrastructure.Events;
using GestorBiblioteca.Infrastructure.Interfaces;
using MediatR;

namespace GestorBiblioteca.Application.Queries.Emprestimos
{
    public class GetAllEmprestimosQuery : IRequest<ResultViewModel<List<EmprestimoViewModel>>>
    {
        public GetAllEmprestimosQuery(string? search = null)
        {
            Search = search;
        }
        public string? Search { get; }
    }

    public class GetAllEmprestimosQueryHandler : IRequestHandler<GetAllEmprestimosQuery, ResultViewModel<List<EmprestimoViewModel>>>
    {
        private readonly IEmprestimoMongoRepository _emprestimoMongoRepository;
        public GetAllEmprestimosQueryHandler(IBaseMongoContext context)
        {
            _emprestimoMongoRepository = new EmprestimoMongoEventsRepository(context, erpName: "Emprestimo");
        }
        public async Task<ResultViewModel<List<EmprestimoViewModel>>> Handle(GetAllEmprestimosQuery request, CancellationToken cancellationToken)
        {
            var listaEmpresa = _emprestimoMongoRepository.BuscarTodos().ToList().Select(e => new EmprestimoViewModel
            {
                Id = e.Id,
                LivroId = e.LivroId,
                TituloLivro = e.Livro?.Titulo ?? string.Empty,
                DataEmprestimo = e.DataEmprestimo,
                DataDevolucao = e.Status == EmprestimoStatusEnum.Devolvido ? e.DataDevolucao : null,
                Status = e.Status.ToString()
            });

            if (!listaEmpresa.Any())
            {
                return ResultViewModel<List<EmprestimoViewModel>>.Error("Sem dados.");
            }
           
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                string search = request.Search.ToLower();
                listaEmpresa = listaEmpresa.Where(l => l.TituloLivro.ToLower().Contains(search));
            }
            return ResultViewModel<List<EmprestimoViewModel>>.Success(listaEmpresa.ToList());
        }
    }
}