using GestorBiblioteca.Application.ViewModels;
using GestorBiblioteca.Domain.Enums;
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
        private readonly IEmprestimoRepository _emprestimoRepository;
        public GetAllEmprestimosQueryHandler(IEmprestimoRepository emprestimoRepository)
        {
            _emprestimoRepository = emprestimoRepository;
        }
        public async Task<ResultViewModel<List<EmprestimoViewModel>>> Handle(GetAllEmprestimosQuery request, CancellationToken cancellationToken)
        {
            var emprestimos = await _emprestimoRepository.BuscarTodos();
            var list = emprestimos.Select(e => new EmprestimoViewModel
            {
                Id = e.Id,
                LivroId = e.LivroId,
                TituloLivro = e.Livro?.Titulo ?? string.Empty,
                DataEmprestimo = e.DataEmprestimo,
                DataDevolucao = e.Status == EmprestimoStatusEnum.Devolvido ? e.DataDevolucao : null,
                Status = e.Status.ToString()
            });
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                string search = request.Search.ToLower();
                list = list.Where(l => l.TituloLivro.ToLower().Contains(search));
            }
            return ResultViewModel<List<EmprestimoViewModel>>.Success(list.ToList());
        }
    }
}