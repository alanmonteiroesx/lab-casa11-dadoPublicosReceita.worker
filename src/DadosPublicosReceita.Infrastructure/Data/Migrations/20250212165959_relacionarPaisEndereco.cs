using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosPublicosReceita.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class relacionarPaisEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Pais_PaisId",
                table: "Endereco");

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Pais_PaisId",
                table: "Endereco",
                column: "PaisId",
                principalTable: "Pais",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Pais_PaisId",
                table: "Endereco");

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Pais_PaisId",
                table: "Endereco",
                column: "PaisId",
                principalTable: "Pais",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
