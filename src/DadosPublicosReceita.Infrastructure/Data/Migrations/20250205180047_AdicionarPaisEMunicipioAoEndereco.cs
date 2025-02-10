using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosPublicosReceita.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarPaisEMunicipioAoEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Numero1",
                table: "Telefone");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Telefone",
                type: "VARCHAR(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "Endereco",
                type: "NVARCHAR(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "MunicipioId",
                table: "Endereco",
                type: "CHAR(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaisId",
                table: "Endereco",
                type: "CHAR(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_MunicipioId",
                table: "Endereco",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_PaisId",
                table: "Endereco",
                column: "PaisId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Municipio_MunicipioId",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Pais_PaisId",
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
                name: "Numero",
                table: "Telefone",
                type: "VARCHAR(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(9)",
                oldMaxLength: 9);

            migrationBuilder.AddColumn<string>(
                name: "Numero1",
                table: "Telefone",
                type: "VARCHAR(9)",
                maxLength: 9,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "Endereco",
                type: "NVARCHAR(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(250)",
                oldMaxLength: 250);
        }
    }
}
