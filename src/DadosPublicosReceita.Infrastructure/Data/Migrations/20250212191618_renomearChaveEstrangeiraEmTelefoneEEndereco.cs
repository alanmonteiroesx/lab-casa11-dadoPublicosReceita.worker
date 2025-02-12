using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosPublicosReceita.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renomearChaveEstrangeiraEmTelefoneEEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Estabelecimento_EstabelecimentoId_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Telefone_Estabelecimento_EstabelecimentoId_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Telefone");

            migrationBuilder.RenameColumn(
                name: "EstabelecimentoId",
                table: "Telefone",
                newName: "EstabelecimentoCnpjBasico");

            migrationBuilder.RenameIndex(
                name: "IX_Telefone_EstabelecimentoId_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Telefone",
                newName: "IX_Telefone_EstabelecimentoCnpjBasico_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv");

            migrationBuilder.RenameColumn(
                name: "EstabelecimentoId",
                table: "Endereco",
                newName: "EstabelecimentoCnpjBasico");

            migrationBuilder.RenameIndex(
                name: "IX_Endereco_EstabelecimentoId_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Endereco",
                newName: "IX_Endereco_EstabelecimentoCnpjBasico_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Pais",
                type: "CHAR(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(4)",
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Municipio",
                type: "CHAR(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(5)",
                oldMaxLength: 5);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Estabelecimento_EstabelecimentoCnpjBasico_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Endereco",
                columns: new[] { "EstabelecimentoCnpjBasico", "EstabelecimentoCnpjOrdem", "EstabelecimentoCnpjDv" },
                principalTable: "Estabelecimento",
                principalColumns: new[] { "CnpjBasico", "CnpjOrdem", "CnpjDv" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Telefone_Estabelecimento_EstabelecimentoCnpjBasico_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Telefone",
                columns: new[] { "EstabelecimentoCnpjBasico", "EstabelecimentoCnpjOrdem", "EstabelecimentoCnpjDv" },
                principalTable: "Estabelecimento",
                principalColumns: new[] { "CnpjBasico", "CnpjOrdem", "CnpjDv" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Estabelecimento_EstabelecimentoCnpjBasico_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Telefone_Estabelecimento_EstabelecimentoCnpjBasico_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Telefone");

            migrationBuilder.RenameColumn(
                name: "EstabelecimentoCnpjBasico",
                table: "Telefone",
                newName: "EstabelecimentoId");

            migrationBuilder.RenameIndex(
                name: "IX_Telefone_EstabelecimentoCnpjBasico_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Telefone",
                newName: "IX_Telefone_EstabelecimentoId_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv");

            migrationBuilder.RenameColumn(
                name: "EstabelecimentoCnpjBasico",
                table: "Endereco",
                newName: "EstabelecimentoId");

            migrationBuilder.RenameIndex(
                name: "IX_Endereco_EstabelecimentoCnpjBasico_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Endereco",
                newName: "IX_Endereco_EstabelecimentoId_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Pais",
                type: "VARCHAR(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "CHAR(3)",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Municipio",
                type: "VARCHAR(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "CHAR(4)",
                oldMaxLength: 4);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Estabelecimento_EstabelecimentoId_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Endereco",
                columns: new[] { "EstabelecimentoId", "EstabelecimentoCnpjOrdem", "EstabelecimentoCnpjDv" },
                principalTable: "Estabelecimento",
                principalColumns: new[] { "CnpjBasico", "CnpjOrdem", "CnpjDv" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Telefone_Estabelecimento_EstabelecimentoId_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Telefone",
                columns: new[] { "EstabelecimentoId", "EstabelecimentoCnpjOrdem", "EstabelecimentoCnpjDv" },
                principalTable: "Estabelecimento",
                principalColumns: new[] { "CnpjBasico", "CnpjOrdem", "CnpjDv" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
