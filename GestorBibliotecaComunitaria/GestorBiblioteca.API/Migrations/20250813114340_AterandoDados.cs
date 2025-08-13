using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorBiblioteca.API.Migrations
{
    /// <inheritdoc />
    public partial class AterandoDados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdEmprestimo",
                table: "Livros");

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeCadastrada",
                table: "Livros",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantidadeCadastrada",
                table: "Livros");

            migrationBuilder.AddColumn<int>(
                name: "IdEmprestimo",
                table: "Livros",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
