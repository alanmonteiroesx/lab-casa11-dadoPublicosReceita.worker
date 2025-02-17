using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosPublicosReceita.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class atualizarRelacaoEmpresaEstabelecimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estabelecimento_Empresa_EmpresaId",
                table: "Estabelecimento");

            migrationBuilder.DropIndex(
                name: "IX_Estabelecimento_EmpresaId",
                table: "Estabelecimento");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Estabelecimento");

            migrationBuilder.AddForeignKey(
                name: "FK_Estabelecimento_Empresa_CnpjBasico",
                table: "Estabelecimento",
                column: "CnpjBasico",
                principalTable: "Empresa",
                principalColumn: "CnpjBasico",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estabelecimento_Empresa_CnpjBasico",
                table: "Estabelecimento");

            migrationBuilder.AddColumn<string>(
                name: "EmpresaId",
                table: "Estabelecimento",
                type: "CHAR(8)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estabelecimento_EmpresaId",
                table: "Estabelecimento",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estabelecimento_Empresa_EmpresaId",
                table: "Estabelecimento",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "CnpjBasico");
        }
    }
}
