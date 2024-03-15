using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmoxarifadoSmart.Infrastructure.Migrations
{
    public partial class DatabaseV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaMotivo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaMotivo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    cargo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    id_departamento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LOGROBO",
                columns: table => new
                {
                    iDlOG = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoRobo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioRobo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateLog = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Etapa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InformacaoLog = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProdutoAPI = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOGROBO", x => x.iDlOG);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    preco = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    estoque_atual = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    estoque_minimo = table.Column<int>(type: "int", nullable: false),
                    BranchmarkingId = table.Column<int>(type: "int", nullable: true),
                    ProdutoScraperModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Motivo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    id_categoriamotivo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motivo", x => x.id);
                    table.ForeignKey(
                        name: "FK__Motivo__id_categ__45F365D3",
                        column: x => x.id_categoriamotivo,
                        principalTable: "CategoriaMotivo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Requisicao",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    prioridade = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    id_departamento = table.Column<int>(type: "int", nullable: false),
                    id_funcionario = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requisicao", x => x.id);
                    table.ForeignKey(
                        name: "FK__Requisica__id_de__440B1D61",
                        column: x => x.id_departamento,
                        principalTable: "Departamento",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Requisica__id_fu__44FF419A",
                        column: x => x.id_funcionario,
                        principalTable: "Funcionario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Benchmarkings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Loja = table.Column<int>(type: "int", nullable: false),
                    Economia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdProduto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benchmarkings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Benchmarking",
                        column: x => x.IdProduto,
                        principalTable: "Produto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoScraper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Loja = table.Column<int>(type: "Int", nullable: false),
                    IdProduto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoScraper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoScraper_Produto",
                        column: x => x.IdProduto,
                        principalTable: "Produto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoRequisicao",
                columns: table => new
                {
                    id_produto = table.Column<int>(type: "int", nullable: false),
                    id_requisicao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProdutoR__9781356BD7E2EC26", x => new { x.id_produto, x.id_requisicao });
                    table.ForeignKey(
                        name: "FK__ProdutoRe__id_pr__4F7CD00D",
                        column: x => x.id_produto,
                        principalTable: "Produto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ProdutoRe__id_re__4316F928",
                        column: x => x.id_requisicao,
                        principalTable: "Requisicao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreProdutos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Store = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProdutoScraperModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProdutos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreProdutos_ProdutoScraper_ProdutoScraperModelId",
                        column: x => x.ProdutoScraperModelId,
                        principalTable: "ProdutoScraper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Benchmarkings_IdProduto",
                table: "Benchmarkings",
                column: "IdProduto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motivo_id_categoriamotivo",
                table: "Motivo",
                column: "id_categoriamotivo");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoRequisicao_id_requisicao",
                table: "ProdutoRequisicao",
                column: "id_requisicao");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoScraper_IdProduto",
                table: "ProdutoScraper",
                column: "IdProduto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requisicao_id_departamento",
                table: "Requisicao",
                column: "id_departamento");

            migrationBuilder.CreateIndex(
                name: "IX_Requisicao_id_funcionario",
                table: "Requisicao",
                column: "id_funcionario");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProdutos_ProdutoScraperModelId",
                table: "StoreProdutos",
                column: "ProdutoScraperModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Benchmarkings");

            migrationBuilder.DropTable(
                name: "LOGROBO");

            migrationBuilder.DropTable(
                name: "Motivo");

            migrationBuilder.DropTable(
                name: "ProdutoRequisicao");

            migrationBuilder.DropTable(
                name: "StoreProdutos");

            migrationBuilder.DropTable(
                name: "CategoriaMotivo");

            migrationBuilder.DropTable(
                name: "Requisicao");

            migrationBuilder.DropTable(
                name: "ProdutoScraper");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Produto");
        }
    }
}
