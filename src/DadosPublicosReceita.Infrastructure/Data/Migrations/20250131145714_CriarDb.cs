using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosPublicosReceita.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriarDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cnae",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "VARCHAR(7)", maxLength: 7, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cnae", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    CnpjBasico = table.Column<string>(type: "CHAR(8)", maxLength: 8, nullable: false),
                    RazaoSocial = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    NaturezaJuridica = table.Column<string>(type: "CHAR(4)", maxLength: 4, nullable: false),
                    QualificacaoResponsavel = table.Column<string>(type: "CHAR(2)", maxLength: 2, nullable: false),
                    CapitalSocial = table.Column<decimal>(type: "MONEY", precision: 18, scale: 4, nullable: false),
                    EnteFederativoResp = table.Column<string>(type: "VARCHAR(4)", maxLength: 4, nullable: true),
                    PorteEmpresa = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.CnpjBasico);
                });

            migrationBuilder.CreateTable(
                name: "Municipio",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "CHAR(4)", maxLength: 4, nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipio", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "CHAR(3)", maxLength: 3, nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Estabelecimento",
                columns: table => new
                {
                    CnpjBasico = table.Column<string>(type: "CHAR(8)", maxLength: 8, nullable: false),
                    CnpjOrdem = table.Column<string>(type: "CHAR(4)", maxLength: 4, nullable: false),
                    CnpjDv = table.Column<string>(type: "CHAR(2)", maxLength: 2, nullable: false),
                    TipoDoEstabelecimento = table.Column<byte>(type: "TINYINT", nullable: false),
                    NomeFantasia = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
                    DataInicioAtividade = table.Column<DateOnly>(type: "DATE", nullable: false),
                    SituacaoCadastral = table.Column<byte>(type: "TINYINT", nullable: false),
                    DataSituacaoCadastral = table.Column<DateOnly>(type: "DATE", nullable: false),
                    MotivoSituacaoCadastral = table.Column<string>(type: "VARCHAR(4)", maxLength: 4, nullable: false),
                    SituacaoEspecial = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: true),
                    DataSituacaoEspecial = table.Column<DateOnly>(type: "DATE", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    CnaePrincipalId = table.Column<string>(type: "VARCHAR(7)", maxLength: 7, nullable: false),
                    EnderecoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estabelecimento", x => x.CnpjBasico);
                    table.ForeignKey(
                        name: "FK_Estabelecimento_Cnae_CnaePrincipalId",
                        column: x => x.CnaePrincipalId,
                        principalTable: "Cnae",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estabelecimento_Empresa_CnpjBasico",
                        column: x => x.CnpjBasico,
                        principalTable: "Empresa",
                        principalColumn: "CnpjBasico");
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoLogradouro = table.Column<string>(type: "NVARCHAR(40)", maxLength: 40, nullable: false),
                    Logradouro = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Numero = table.Column<string>(type: "NVARCHAR(10)", maxLength: 10, nullable: false),
                    Complemento = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    Bairro = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Cep = table.Column<string>(type: "CHAR(8)", maxLength: 8, nullable: false),
                    Uf = table.Column<string>(type: "CHAR(2)", maxLength: 2, nullable: false),
                    MunicipioId = table.Column<string>(type: "CHAR(4)", nullable: false),
                    EstabelecimentoId = table.Column<string>(type: "CHAR(8)", nullable: false),
                    PaisId = table.Column<string>(type: "CHAR(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Estabelecimento_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
                        principalTable: "Estabelecimento",
                        principalColumn: "CnpjBasico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Endereco_Municipio_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipio",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Endereco_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Telefone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false),
                    EstabelecimentoId = table.Column<string>(type: "CHAR(8)", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefone_Estabelecimento_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
                        principalTable: "Estabelecimento",
                        principalColumn: "CnpjBasico",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_RazaoSocial",
                table: "Empresa",
                column: "RazaoSocial",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Estabelecimento_CnaePrincipalId",
                table: "Estabelecimento",
                column: "CnaePrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_EstabelecimentoId",
                table: "Telefone",
                column: "EstabelecimentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Telefone");

            migrationBuilder.DropTable(
                name: "Municipio");

            migrationBuilder.DropTable(
                name: "Pais");

            migrationBuilder.DropTable(
                name: "Estabelecimento");

            migrationBuilder.DropTable(
                name: "Cnae");

            migrationBuilder.DropTable(
                name: "Empresa");
        }
    }
}
