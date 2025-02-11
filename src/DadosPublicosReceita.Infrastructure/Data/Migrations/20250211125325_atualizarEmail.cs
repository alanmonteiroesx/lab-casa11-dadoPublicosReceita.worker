using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosPublicosReceita.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class atualizarEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Pais_PaisId",
                table: "Endereco");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Estabelecimento",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "ImportacaoControle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnoMes = table.Column<string>(type: "varchar(7)", nullable: false),
                    NomeArquivo = table.Column<string>(type: "varchar(100)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "datetime", nullable: true),
                    QuantidadeRegistros = table.Column<int>(type: "int", nullable: false),
                    UltimoRegistroProcessado = table.Column<int>(type: "int", nullable: false),
                    Erro = table.Column<string>(type: "varchar(max)", nullable: true),
                    VersaoArquivo = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportacaoControle", x => x.Id);
                });

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
                name: "FK_Endereco_Pais_PaisId",
                table: "Endereco",
                column: "PaisId",
                principalTable: "Pais",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Pais_PaisId",
                table: "Endereco");

            migrationBuilder.DropTable(
                name: "ImportacaoControle");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Estabelecimento",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(150)",
                oldMaxLength: 150);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Pais_PaisId",
                table: "Endereco",
                column: "PaisId",
                principalTable: "Pais",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
