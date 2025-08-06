using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using GestorBiblioteca.Application.Commands.Requests.Emprestimo;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Application.Handlers;
using GestorBiblioteca.Application.Handlers.Emprestimo;
using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Domain.Enums;
using GestorBiblioteca.Infrastructure.Interfaces;
using NSubstitute;
using Xunit;

namespace GestorBiblioteca.Tests.Application
{
    /// <summary>
    /// Tests for CreateEmprestimoHandler.  This handler performs
    /// validation on the incoming request, fetches the Livro from
    /// repository and attempts to persist a new Emprestimo.  Responses
    /// indicate failure reasons or success along with the created entity.
    /// </summary>
    public class CreateEmprestimoHandlerTests
    {
        private static Livro CreateLivro() => new("Titulo", "Autor", 2020, 2);

        private static void SetRequestLivro(CreateEmprestimoRequest request, Livro livro)
        {
            // Use reflection to set the private setter on the Livro property to simulate a non-null book in the request
            var property = typeof(CreateEmprestimoRequest).GetProperty("Livro", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            property!.SetValue(request, livro);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenLivroIdIsInvalid()
        {
            // Arrange
            var emprestimoRepository = Substitute.For<IEmprestimoRepository>();
            var livroRepository = Substitute.For<ILivroRepository>();
            var handler = new CreateEmprestimoHandler(emprestimoRepository, livroRepository);
            var request = new CreateEmprestimoRequest(0, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1), EmprestimoStatusEnum.Ativo);

            // Act
            GenericCommandResponse response = await handler.Handle(request, CancellationToken.None);

            // Assert
            response.Sucesso.Should().BeFalse();
            response.Menssagem.Should().Be("Identificador do livro invalido.");
            response.Dados.Should().BeNull();
        }


        [Fact]
        public async Task Handle_ShouldReturnError_WhenRequestLivroIsNull()
        {
            // Arrange
            var emprestimoRepository = Substitute.For<IEmprestimoRepository>();
            var livroRepository = Substitute.For<ILivroRepository>();
            var livroFromRepo = CreateLivro();
            livroRepository.BuscarPorId(Arg.Any<int>()).Returns(Task.FromResult(livroFromRepo));

            var handler = new CreateEmprestimoHandler(emprestimoRepository, livroRepository);
            var request = new CreateEmprestimoRequest(1, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1), EmprestimoStatusEnum.Ativo);
            // Do not set Livro on request -> remains null

            // Act
            GenericCommandResponse response = await handler.Handle(request, CancellationToken.None);

            // Assert
            response.Sucesso.Should().BeFalse();
            response.Menssagem.Should().Be("Livro não pode ser vazio");
            response.Dados.Should().BeNull();
        }

  

        
    }
}