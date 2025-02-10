using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosPublicosReceita.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarRelacionamentosEstabelecimentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Estabelecimento_EstabelecimentoId",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Municipio_MunicipioId",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Pais_PaisId",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Estabelecimento_Empresa_CnpjBasico",
                table: "Estabelecimento");

            migrationBuilder.DropForeignKey(
                name: "FK_Telefone_Estabelecimento_EstabelecimentoId",
                table: "Telefone");

            migrationBuilder.DropIndex(
                name: "IX_Telefone_EstabelecimentoId",
                table: "Telefone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estabelecimento",
                table: "Estabelecimento");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_EstabelecimentoId",
                table: "Endereco");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_MunicipioId",
                table: "Endereco");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_PaisId",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "MunicipioId",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "PaisId",
                table: "Endereco");

            migrationBuilder.AlterColumn<string>(
                name: "EstabelecimentoId",
                table: "Telefone",
                type: "CHAR(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "CHAR(8)",
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Telefone",
                type: "VARCHAR(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EstabelecimentoCnpjDv",
                table: "Telefone",
                type: "CHAR(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EstabelecimentoCnpjOrdem",
                table: "Telefone",
                type: "CHAR(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Numero1",
                table: "Telefone",
                type: "VARCHAR(9)",
                maxLength: 9,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "EmpresaId",
                table: "Estabelecimento",
                type: "CHAR(8)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstabelecimentoCnpjDv",
                table: "Endereco",
                type: "CHAR(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EstabelecimentoCnpjOrdem",
                table: "Endereco",
                type: "CHAR(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estabelecimento",
                table: "Estabelecimento",
                columns: new[] { "CnpjBasico", "CnpjOrdem", "CnpjDv" });

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_EstabelecimentoId_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Telefone",
                columns: new[] { "EstabelecimentoId", "EstabelecimentoCnpjOrdem", "EstabelecimentoCnpjDv" });

            migrationBuilder.CreateIndex(
                name: "IX_Estabelecimento_EmpresaId",
                table: "Estabelecimento",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_EstabelecimentoId_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Endereco",
                columns: new[] { "EstabelecimentoId", "EstabelecimentoCnpjOrdem", "EstabelecimentoCnpjDv" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Estabelecimento_EstabelecimentoId_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Endereco",
                columns: new[] { "EstabelecimentoId", "EstabelecimentoCnpjOrdem", "EstabelecimentoCnpjDv" },
                principalTable: "Estabelecimento",
                principalColumns: new[] { "CnpjBasico", "CnpjOrdem", "CnpjDv" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estabelecimento_Empresa_EmpresaId",
                table: "Estabelecimento",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "CnpjBasico");

            migrationBuilder.AddForeignKey(
                name: "FK_Telefone_Estabelecimento_EstabelecimentoId_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Telefone",
                columns: new[] { "EstabelecimentoId", "EstabelecimentoCnpjOrdem", "EstabelecimentoCnpjDv" },
                principalTable: "Estabelecimento",
                principalColumns: new[] { "CnpjBasico", "CnpjOrdem", "CnpjDv" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Estabelecimento_EstabelecimentoId_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Estabelecimento_Empresa_EmpresaId",
                table: "Estabelecimento");

            migrationBuilder.DropForeignKey(
                name: "FK_Telefone_Estabelecimento_EstabelecimentoId_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Telefone");

            migrationBuilder.DropIndex(
                name: "IX_Telefone_EstabelecimentoId_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Telefone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estabelecimento",
                table: "Estabelecimento");

            migrationBuilder.DropIndex(
                name: "IX_Estabelecimento_EmpresaId",
                table: "Estabelecimento");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_EstabelecimentoId_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Telefone");

            migrationBuilder.DropColumn(
                name: "EstabelecimentoCnpjDv",
                table: "Telefone");

            migrationBuilder.DropColumn(
                name: "EstabelecimentoCnpjOrdem",
                table: "Telefone");

            migrationBuilder.DropColumn(
                name: "Numero1",
                table: "Telefone");

            migrationBuilder.DropColumn(
                name: "EstabelecimentoCnpjDv",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "EstabelecimentoCnpjOrdem",
                table: "Endereco");

            migrationBuilder.AlterColumn<string>(
                name: "EstabelecimentoId",
                table: "Telefone",
                type: "CHAR(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "CHAR(8)",
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "EmpresaId",
                table: "Estabelecimento",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "CHAR(8)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MunicipioId",
                table: "Endereco",
                type: "CHAR(4)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaisId",
                table: "Endereco",
                type: "CHAR(3)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estabelecimento",
                table: "Estabelecimento",
                column: "CnpjBasico");

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_EstabelecimentoId",
                table: "Telefone",
                column: "EstabelecimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_EstabelecimentoId",
                table: "Endereco",
                column: "EstabelecimentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_MunicipioId",
                table: "Endereco",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_PaisId",
                table: "Endereco",
                column: "PaisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Estabelecimento_EstabelecimentoId",
                table: "Endereco",
                column: "EstabelecimentoId",
                principalTable: "Estabelecimento",
                principalColumn: "CnpjBasico",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Municipio_MunicipioId",
                table: "Endereco",
                column: "MunicipioId",
                principalTable: "Municipio",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Pais_PaisId",
                table: "Endereco",
                column: "PaisId",
                principalTable: "Pais",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estabelecimento_Empresa_CnpjBasico",
                table: "Estabelecimento",
                column: "CnpjBasico",
                principalTable: "Empresa",
                principalColumn: "CnpjBasico");

            migrationBuilder.AddForeignKey(
                name: "FK_Telefone_Estabelecimento_EstabelecimentoId",
                table: "Telefone",
                column: "EstabelecimentoId",
                principalTable: "Estabelecimento",
                principalColumn: "CnpjBasico",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
