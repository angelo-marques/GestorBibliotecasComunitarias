using System;
using FluentAssertions;
using GestorBiblioteca.Domain.Entities;
using Xunit;

namespace GestorBiblioteca.Tests.Domain
{
    public class BaseEntityTests
    {
        private sealed class TestEntity : BaseEntity
        {
        }

        [Fact]
        public void Constructor_ShouldInitializeCreatedAt_And_IsDeletedFalse()
        {
            // Act
            var entity = new TestEntity();

            // Assert
            entity.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(2));
            entity.IsDeleted.Should().BeFalse();
        }

        [Fact]
        public void SetAsDeleted_ShouldMarkEntityAsDeleted()
        {
            // Arrange
            var entity = new TestEntity();

            // Act
            entity.SetAsDeleted();

            // Assert
            entity.IsDeleted.Should().BeTrue();
        }
    }
}