﻿using GestorBiblioteca.Infrastructure.Persistence;
using GestorBibliotecaApplication.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBibliotecaApplication.Commands.UpdateLivro
{
    public class UpdateLivroCommandHandler : IRequestHandler<UpdateLivroCommand, ResultViewModel>
    {
        private readonly GestorBibliotecaDbContext _livrosDbContext;

        public UpdateLivroCommandHandler(GestorBibliotecaDbContext livrosDbContext)
        {
            _livrosDbContext = livrosDbContext;
        }

        public async Task <ResultViewModel> Handle(UpdateLivroCommand request, CancellationToken cancellationToken)
        {
            var livro = await _livrosDbContext.Livros.SingleOrDefaultAsync(l => l.Id == request.Id);

            if (livro == null)
                return ResultViewModel.Error("Livro inexistente");

            livro.Update(request.Autor, request.Titulo, request.ISBN, request.AnoPublicacao);
            _livrosDbContext.Livros.Update(livro);
            await _livrosDbContext.SaveChangesAsync();
            return ResultViewModel.Sucess();
        }
    }
}
