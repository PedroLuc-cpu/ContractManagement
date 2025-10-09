using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addTableItemPedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdCliente",
                table: "pedido",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "pedido",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "itempedido",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_pedidos = table.Column<Guid>(type: "uuid", nullable: false),
                    Produto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    pedidoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itempedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_itempedido_pedido_pedidoId",
                        column: x => x.pedidoId,
                        principalTable: "pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pedido_Numero",
                table: "pedido",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_itempedido_pedidoId",
                table: "itempedido",
                column: "pedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "itempedido");

            migrationBuilder.DropIndex(
                name: "IX_pedido_Numero",
                table: "pedido");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "pedido");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "pedido");
        }
    }
}
