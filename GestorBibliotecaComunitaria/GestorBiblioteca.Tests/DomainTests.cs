using GestorBiblioteca.Domain.Entities;
using GestorBiblioteca.Domain.Enums;
using Xunit;
using System;

namespace GestorBiblioteca.Tests
{
    public class DomainTests
    {
        [Fact]
        public void Livro_Creation_Should_Set_Properties()
        {
            // Arrange
            var titulo = "Dom Quixote";
            var autor = "Miguel de Cervantes";
            var ano = 1605;
            var quantidade = 5;

            // Act
            var livro = new Livro(titulo, autor, ano, quantidade);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(titulo, livro.Titulo);
                Assert.Equal(autor, livro.Autor);
                Assert.Equal(ano, livro.AnoPublicacao);
                Assert.Equal(quantidade, livro.QuantidadeDisponivel);
            });
        }

        [Fact]
        public void Livro_DiminuirQuantidade_Should_Reduce_Stock()
        {
            var livro = new Livro("Title", "Author", 2020, 3);
            livro.DiminuirQuantidade();
            Assert.Equal(2, livro.QuantidadeDisponivel);
        }

        [Fact]
        public void Livro_DiminuirQuantidade_Should_Throw_When_No_Stock()
        {
            var livro = new Livro("Title", "Author", 2020, 1);
            livro.DiminuirQuantidade();
            Assert.Throws<ArgumentException>(() => livro.DiminuirQuantidade());
        }

        [Fact]
        public void Livro_AumentarQuantidade_Should_Increase_Stock()
        {
            var livro = new Livro("Title", "Author", 2020, 1);
            livro.AumentarQuantidade();
            Assert.Equal(2, livro.QuantidadeDisponivel);
        }

        [Fact]
        public void Emprestimo_DevolverLivro_Should_Update_Status_And_Date()
        {
            var livro = new Livro("Title", "Author", 2020, 1);
            var dataEmprestimo = DateTime.UtcNow.AddDays(-2);
            var dueDate = DateTime.UtcNow.AddDays(5);
            var emprestimo = new Emprestimo(1, livro, dataEmprestimo, dueDate, EmprestimoStatusEnum.Ativo);

            emprestimo.DevolverLivro();
            Assert.Multiple(() =>
            {
                Assert.Equal(EmprestimoStatusEnum.Devolvido, emprestimo.Status);
                Assert.True((DateTime.UtcNow - emprestimo.DataDevolucao).TotalSeconds < 1);
            });
        }
    }
}