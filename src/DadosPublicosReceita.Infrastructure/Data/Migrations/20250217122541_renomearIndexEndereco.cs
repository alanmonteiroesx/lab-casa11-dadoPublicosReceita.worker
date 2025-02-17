using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosPublicosReceita.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renomearIndexEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Endereco_PaisId",
                table: "Endereco",
                newName: "IX_Endereco_Pais");

            migrationBuilder.RenameIndex(
                name: "IX_Endereco_MunicipioId",
                table: "Endereco",
                newName: "IX_Endereco_Municipio");

            migrationBuilder.RenameIndex(
                name: "IX_Endereco_EstabelecimentoCnpjBasico_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Endereco",
                newName: "IX_Endereco_Estabelecimento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Endereco_Pais",
                table: "Endereco",
                newName: "IX_Endereco_PaisId");

            migrationBuilder.RenameIndex(
                name: "IX_Endereco_Municipio",
                table: "Endereco",
                newName: "IX_Endereco_MunicipioId");

            migrationBuilder.RenameIndex(
                name: "IX_Endereco_Estabelecimento",
                table: "Endereco",
                newName: "IX_Endereco_EstabelecimentoCnpjBasico_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv");
        }
    }
}
