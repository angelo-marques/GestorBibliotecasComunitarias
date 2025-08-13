using System;
using FluentAssertions;
using GestorBiblioteca.Domain.Entities;
using Xunit;

namespace GestorBiblioteca.Tests.Domain
{
    public class LivroTests
    {
        [Fact]
        public void Constructor_WithValidData_ShouldInitializeProperties()
        {
            // Arrange
            string titulo = "O Senhor dos Anéis";
            string autor = "J.R.R. Tolkien";
            int ano = 1954;
            int quantidade = 10;

            // Act
            var livro = new Livro(titulo, autor, ano, quantidade, 5);

            // Assert
            livro.Titulo.Should().Be(titulo);
            livro.Autor.Should().Be(autor);
            livro.AnoPublicacao.Should().Be(ano);
            livro.QuantidadeDisponivel.Should().Be(quantidade);
            livro.Emprestimos.Should().BeNull();
        }

        [Theory]
        [InlineData(null, "Autor", 2020, 1)]
        [InlineData("", "Autor", 2020, 1)]
        [InlineData("Titulo", null, 2020, 1)]
        [InlineData("Titulo", "", 2020, 1)]
        [InlineData("Titulo", "Autor", 2020, 0)]
        [InlineData("Titulo", "Autor", 2020, -1)]
        public void Constructor_WithInvalidData_ShouldThrow(string titulo, string autor, int ano, int quantidade)
        {
            // Act
            Action act = () => new Livro(titulo, autor, ano, quantidade, 5);

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void DiminuirQuantidade_WithPositiveQuantity_ShouldDecrement()
        {
            // Arrange
            var livro = new Livro("Teste", "Autor", 2000, 5, 5);
            int quantidadeAntes = livro.QuantidadeDisponivel;

            // Act
            livro.DiminuirQuantidade();

            // Assert
            livro.QuantidadeDisponivel.Should().Be(quantidadeAntes - 1);
        }

        [Fact]
        public void DiminuirQuantidade_WithZeroOrNegativeQuantity_ShouldThrow()
        {
            // Arrange
            var livro = new Livro("Teste", "Autor", 2000, 1, 1);
            livro.DiminuirQuantidade();

            // Act
            Action act = () => livro.DiminuirQuantidade();

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Livro indisponível para empréstimo*");
        }

        [Fact]
        public void AumentarQuantidade_ShouldIncrement()
        {
            // Arrange
            var livro = new Livro("Teste", "Autor", 2000, 1, 1);
            int quantidadeAntes = livro.QuantidadeDisponivel;

            // Act
            livro.AumentarQuantidade();

            // Assert
            livro.QuantidadeDisponivel.Should().Be(quantidadeAntes + 1);
        }

        [Fact]
        public void Update_WithValidData_ShouldUpdateFieldsAndDecreaseOnce()
        {
            var livro = new Livro("Titulo original", "Autor original", 2000, 5, 5);
            string novoAutor = "Novo Autor";
            string novoTitulo = "Novo Título";
            int novaQuantidade = 3;
            int novoAno = 2025;

            livro.Update(novoAutor, novoTitulo, novaQuantidade, novoAno);

            livro.Autor.Should().Be(novoAutor);
            livro.Titulo.Should().Be(novoTitulo);
            livro.QuantidadeDisponivel.Should().Be(novaQuantidade);
            livro.AnoPublicacao.Should().Be(novoAno);
        }

        [Theory]
        [InlineData("", "Autor", 3, 2023)]
        [InlineData("Título", "", 3, 2023)]
        [InlineData("Título", "Autor", 0, 2023)]
        [InlineData("Título", "Autor", -1, 2023)]
        public void Update_WithInvalidData_ShouldThrow(string novoAutor, string novoTitulo, int novaQuantidade, int novoAno)
        {
            var livro = new Livro("Titulo", "Autor", 2000, 5, 5);

            Action act = () => livro.Update(novoAutor, novoTitulo, novaQuantidade, novoAno);

            act.Should().Throw<InvalidOperationException>();
        }
    }
}