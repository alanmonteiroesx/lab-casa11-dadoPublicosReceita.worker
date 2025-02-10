using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosPublicosReceita.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NovaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Telefone",
                type: "VARCHAR(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(2)",
                oldMaxLength: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Telefone",
                type: "VARCHAR(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(5)",
                oldMaxLength: 5);
        }
    }
}
