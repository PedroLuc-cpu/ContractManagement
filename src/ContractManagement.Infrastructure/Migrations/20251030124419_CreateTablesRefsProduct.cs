using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTablesRefsProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrecoCusto_Coin",
                table: "produtos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrecoVenda_Coin",
                table: "produtos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ValorTotal_Coin",
                table: "pedido",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrecoUnitario_Coin",
                table: "item_pedido",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "disponibilidade_produto",
                columns: table => new
                {
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    inicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    fim = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disponibilidade_produto", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_disponibilidade_produto_produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "promocao_produto",
                columns: table => new
                {
                    produto_id = table.Column<Guid>(type: "uuid", nullable: false),
                    desconto_percentual = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promocao_produto", x => x.produto_id);
                    table.ForeignKey(
                        name: "FK_promocao_produto_produtos_produto_id",
                        column: x => x.produto_id,
                        principalTable: "produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "disponibilidade_produto");

            migrationBuilder.DropTable(
                name: "promocao_produto");

            migrationBuilder.DropColumn(
                name: "PrecoCusto_Coin",
                table: "produtos");

            migrationBuilder.DropColumn(
                name: "PrecoVenda_Coin",
                table: "produtos");

            migrationBuilder.DropColumn(
                name: "ValorTotal_Coin",
                table: "pedido");

            migrationBuilder.DropColumn(
                name: "PrecoUnitario_Coin",
                table: "item_pedido");
        }
    }
}
