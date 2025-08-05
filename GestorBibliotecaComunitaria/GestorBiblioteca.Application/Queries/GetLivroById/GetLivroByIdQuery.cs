﻿using GestorBiblioteca.Infrastructure.Persistence;
using GestorBibliotecaApplication.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Queries.GetLivroById
{
    public class GetLivroByIdQuery: IRequest<ResultViewModel<LivroDetailsModel>>
    {
        public GetLivroByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    public class GetLivrosByIdQueryHandler : IRequestHandler<GetLivroByIdQuery, ResultViewModel<LivroDetailsModel>>
    {
        private readonly GestorBibliotecaDbContext _livrosDbContext;

        public GetLivrosByIdQueryHandler(GestorBibliotecaDbContext livrosDbContext)
        {
            _livrosDbContext = livrosDbContext;
        }

        public async Task<ResultViewModel<LivroDetailsModel>> Handle(GetLivroByIdQuery request, CancellationToken cancellationToken)
        {
            var livro = await _livrosDbContext.Livros
                                              .SingleOrDefaultAsync(l => l.Id == request.Id && !l.IsDeleted);

            if (livro == null)
                return ResultViewModel<LivroDetailsModel>.Error("Livro inexistente");

            var livroDetailsModel = new LivroDetailsModel
            {
                IdLivro = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                ISBN = livro.Isbn,
                AnoPublicacao = livro.AnoPublicacao,
                Status = livro.Status
            };
            return ResultViewModel<LivroDetailsModel>.Success(livroDetailsModel);
        }
    }
}
