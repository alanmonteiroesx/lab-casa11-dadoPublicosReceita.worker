using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosPublicosReceita.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renomearChaves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Estabelecimento_EstabelecimentoCnpjBasico_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Municipio_MunicipioId",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Pais_PaisId",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Estabelecimento_Cnae_CnaePrincipalId",
                table: "Estabelecimento");

            migrationBuilder.DropForeignKey(
                name: "FK_Estabelecimento_Empresa_CnpjBasico",
                table: "Estabelecimento");

            migrationBuilder.DropForeignKey(
                name: "FK_Telefone_Estabelecimento_EstabelecimentoCnpjBasico_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Telefone");

            migrationBuilder.DropIndex(
                name: "IX_ImportacaoControle_AnoMes_NomeArquivo",
                table: "ImportacaoControle");

            migrationBuilder.DropIndex(
                name: "IX_ImportacaoControle_Status",
                table: "ImportacaoControle");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "ImportacaoControle",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Estabelecimento",
                table: "Endereco",
                columns: new[] { "EstabelecimentoCnpjBasico", "EstabelecimentoCnpjOrdem", "EstabelecimentoCnpjDv" },
                principalTable: "Estabelecimento",
                principalColumns: new[] { "CnpjBasico", "CnpjOrdem", "CnpjDv" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Municipio",
                table: "Endereco",
                column: "MunicipioId",
                principalTable: "Municipio",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Pais",
                table: "Endereco",
                column: "PaisId",
                principalTable: "Pais",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estabelecimento_Cnae",
                table: "Estabelecimento",
                column: "CnaePrincipalId",
                principalTable: "Cnae",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estabelecimento_Empresa",
                table: "Estabelecimento",
                column: "CnpjBasico",
                principalTable: "Empresa",
                principalColumn: "CnpjBasico",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Telefone_Estabelecimento",
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
                name: "FK_Endereco_Estabelecimento",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Municipio",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Pais",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Estabelecimento_Cnae",
                table: "Estabelecimento");

            migrationBuilder.DropForeignKey(
                name: "FK_Estabelecimento_Empresa",
                table: "Estabelecimento");

            migrationBuilder.DropForeignKey(
                name: "FK_Telefone_Estabelecimento",
                table: "Telefone");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ImportacaoControle",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_ImportacaoControle_AnoMes_NomeArquivo",
                table: "ImportacaoControle",
                columns: new[] { "AnoMes", "NomeArquivo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImportacaoControle_Status",
                table: "ImportacaoControle",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Estabelecimento_EstabelecimentoCnpjBasico_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Endereco",
                columns: new[] { "EstabelecimentoCnpjBasico", "EstabelecimentoCnpjOrdem", "EstabelecimentoCnpjDv" },
                principalTable: "Estabelecimento",
                principalColumns: new[] { "CnpjBasico", "CnpjOrdem", "CnpjDv" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Municipio_MunicipioId",
                table: "Endereco",
                column: "MunicipioId",
                principalTable: "Municipio",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Pais_PaisId",
                table: "Endereco",
                column: "PaisId",
                principalTable: "Pais",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estabelecimento_Cnae_CnaePrincipalId",
                table: "Estabelecimento",
                column: "CnaePrincipalId",
                principalTable: "Cnae",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estabelecimento_Empresa_CnpjBasico",
                table: "Estabelecimento",
                column: "CnpjBasico",
                principalTable: "Empresa",
                principalColumn: "CnpjBasico",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Telefone_Estabelecimento_EstabelecimentoCnpjBasico_EstabelecimentoCnpjOrdem_EstabelecimentoCnpjDv",
                table: "Telefone",
                columns: new[] { "EstabelecimentoCnpjBasico", "EstabelecimentoCnpjOrdem", "EstabelecimentoCnpjDv" },
                principalTable: "Estabelecimento",
                principalColumns: new[] { "CnpjBasico", "CnpjOrdem", "CnpjDv" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
