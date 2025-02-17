using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosPublicosReceita.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renomearIndexEstabelecimnetoETelefone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Telefone_EstabelecimentoCnpjBasico_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Telefone",
                newName: "IX_Telefone_Estabelecimento");

            migrationBuilder.RenameIndex(
                name: "IX_Estabelecimento_CnaePrincipalId",
                table: "Estabelecimento",
                newName: "IX_Estabelecimento_Cnae");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Telefone_Estabelecimento",
                table: "Telefone",
                newName: "IX_Telefone_EstabelecimentoCnpjBasico_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv");

            migrationBuilder.RenameIndex(
                name: "IX_Estabelecimento_Cnae",
                table: "Estabelecimento",
                newName: "IX_Estabelecimento_CnaePrincipalId");
        }
    }
}
