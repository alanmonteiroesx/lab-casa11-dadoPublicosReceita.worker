using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosPublicosReceita.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarEmpresas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Empresa_RazaoSocial",
                table: "Empresa");

            migrationBuilder.AlterColumn<string>(
                name: "RazaoSocial",
                table: "Empresa",
                type: "NVARCHAR(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "EnteFederativoResp",
                table: "Empresa",
                type: "NVARCHAR(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(4)",
                oldMaxLength: 4,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_RazaoSocial",
                table: "Empresa",
                column: "RazaoSocial");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Empresa_RazaoSocial",
                table: "Empresa");

            migrationBuilder.AlterColumn<string>(
                name: "RazaoSocial",
                table: "Empresa",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "EnteFederativoResp",
                table: "Empresa",
                type: "VARCHAR(4)",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_RazaoSocial",
                table: "Empresa",
                column: "RazaoSocial",
                unique: true);
        }
    }
}
