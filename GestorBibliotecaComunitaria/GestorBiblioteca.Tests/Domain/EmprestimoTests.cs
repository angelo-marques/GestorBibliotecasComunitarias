using System;
using FluentAssertions;
using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Domain.Enums;
using Xunit;

namespace GestorBiblioteca.Tests.Domain
{
    public class EmprestimoTests
    {
        private static Livro CreateLivro() => new("Test Title", "Test Author", 2000, 5);

        [Fact]
        public void Constructor_WithValidData_ShouldSetProperties()
        {
            // Arrange
            var livro = CreateLivro();
            int livroId = 1;
            DateTime dataEmprestimo = new DateTime(2024, 1, 1);
            DateTime dataDevolucao = new DateTime(2024, 1, 2);
            EmprestimoStatusEnum status = EmprestimoStatusEnum.Ativo;

            // Act
            var emprestimo = new Emprestimo(livroId, livro, dataEmprestimo, dataDevolucao, status);

            // Assert
            emprestimo.LivroId.Should().Be(livroId);
            emprestimo.Livro.Should().Be(livro);
            emprestimo.DataEmprestimo.Should().Be(dataEmprestimo);
            emprestimo.DataDevolucao.Should().Be(dataDevolucao);
            emprestimo.Status.Should().Be(status);
        }

        [Fact]
        public void Constructor_WithEmprestimoDateAfterDevolucao_ShouldThrow()
        {
            // Arrange
            var livro = CreateLivro();
            int livroId = 1;
            DateTime dataEmprestimo = new DateTime(2024, 1, 2);
            DateTime dataDevolucao = new DateTime(2024, 1, 1);

            // Act
            Action act = () => new Emprestimo(livroId, livro, dataEmprestimo, dataDevolucao, EmprestimoStatusEnum.Ativo);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("Data de emprestimo esta maior que a data de devolução*");
        }

        [Fact]
        public void Constructor_WithNullLivro_ShouldThrow()
        {
            // Arrange
            Livro livro = null;
            int livroId = 1;
            DateTime dataEmprestimo = new DateTime(2024, 1, 1);
            DateTime dataDevolucao = new DateTime(2024, 1, 2);

            // Act
            Action act = () => new Emprestimo(livroId, livro, dataEmprestimo, dataDevolucao, EmprestimoStatusEnum.Ativo);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("Sem livro.*");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(-1)]
        public void Constructor_WithInvalidStatus_ShouldThrow(int invalidStatus)
        {
            // Arrange
            var livro = CreateLivro();
            int livroId = 1;
            DateTime dataEmprestimo = new DateTime(2024, 1, 1);
            DateTime dataDevolucao = new DateTime(2024, 1, 2);
            var status = (EmprestimoStatusEnum)invalidStatus;

            // Act
            Action act = () => new Emprestimo(livroId, livro, dataEmprestimo, dataDevolucao, status);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("Status invalido.*");
        }

        [Fact]
        public void DevolverLivro_ShouldSetStatusToDevolvidoAndUpdateDate()
        {
            // Arrange
            var livro = CreateLivro();
            var emprestimo = new Emprestimo(1, livro, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(1), EmprestimoStatusEnum.Ativo);

            // Act
            emprestimo.DevolverLivro();

            // Assert
            emprestimo.Status.Should().Be(EmprestimoStatusEnum.Devolvido);
            // DataDevolucao should be set to near now (UTC)
            emprestimo.DataDevolucao.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        }

        [Fact]
        public void DevolverLivro_WhenAlreadyReturned_ShouldThrow()
        {
            // Arrange
            var livro = CreateLivro();
            var emprestimo = new Emprestimo(1, livro, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(1), EmprestimoStatusEnum.Ativo);
            emprestimo.DevolverLivro();

            // Act
            Action act = () => emprestimo.DevolverLivro();

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Empréstimo já devolvido");
        }
    }
}