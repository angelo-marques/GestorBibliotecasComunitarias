using FluentAssertions;
using GestorBiblioteca.Application.Commands.Requests.Livros;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Application.Handlers.Livro;
using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Infrastructure.Interfaces;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GestorBiblioteca.Tests.Application
{
    public class CreateLivroHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnSuccessResponse_WhenRepositoryPersists()
        {
            // Arrange
            var livroRepository = Substitute.For<ILivroRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            livroRepository.UnitOfWork.Returns(unitOfWork);
            unitOfWork.Commit().Returns(Task.FromResult(true));

            var handler = new CreateLivroHandler(livroRepository);
            var request = new CreateLivroRequest("Titulo", "Autor", 2024, 3);

            // Act
            GenericCommandResponse response = await handler.Handle(request, CancellationToken.None);

            // Assert
            response.Sucesso.Should().BeTrue();
            response.Menssagem.Should().Be("Sucesso");
            response.Dados.Should().NotBeNull().And.BeOfType<Livro>();
            livroRepository.Received(1).Cadastrar(Arg.Any<Livro>());
            await unitOfWork.Received(1).Commit();
        }

        [Fact]
        public async Task Handle_ShouldReturnErrorResponse_WhenCommitFails()
        {
            // Arrange
            var livroRepository = Substitute.For<ILivroRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            livroRepository.UnitOfWork.Returns(unitOfWork);
            unitOfWork.Commit().Returns(Task.FromResult(false));
            var handler = new CreateLivroHandler(livroRepository);
            var request = new CreateLivroRequest("Titulo", "Autor", 2024, 3);

            // Act
            GenericCommandResponse response = await handler.Handle(request, CancellationToken.None);

            // Assert
            response.Sucesso.Should().BeFalse();
            response.Menssagem.Should().Be("Erro ao tentar salvar dados");
            response.Dados.Should().BeNull();
        }
    }
}