using FluentAssertions;
using GestorBiblioteca.Application.Commands.Requests.Emprestimo;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Application.Handlers.Emprestimo;
using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Domain.Enums;
using GestorBiblioteca.Infrastructure.Interfaces;
using GestorBiblioteca.Infrastructure.Persistence;
using NSubstitute;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GestorBiblioteca.Tests.Application
{
    public class CreateEmprestimoHandlerTests
    {
        private static Livro CreateLivro() => new("Titulo", "Autor", 2020, 2, 2);

        private static void SetRequestLivro(CreateEmprestimoRequest request, Livro livro)
        {
            var property = typeof(CreateEmprestimoRequest).GetProperty("Livro", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            property!.SetValue(request, livro);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenLivroIdIsInvalid()
        {
          
            // Arrange
            var emprestimoRepository = Substitute.For<IEmprestimoRepository>();
            var livroRepository = Substitute.For<ILivroRepository>();
            var context = Substitute.For<IBaseMongoContext>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var handler = new CreateEmprestimoHandler(emprestimoRepository, livroRepository, context, unitOfWork);
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
            var context = Substitute.For<IBaseMongoContext>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var handler = new CreateEmprestimoHandler(emprestimoRepository, livroRepository, context, unitOfWork);
            var request = new CreateEmprestimoRequest(1, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(-1), EmprestimoStatusEnum.Ativo);

            // Act
            GenericCommandResponse response = await handler.Handle(request, CancellationToken.None);

            // Assert
            response.Sucesso.Should().BeFalse();
            response.Menssagem.Should().Be("Data de emprestimo esta maior que a data de devolução");
            response.Dados.Should().BeNull();
        }
    }
}