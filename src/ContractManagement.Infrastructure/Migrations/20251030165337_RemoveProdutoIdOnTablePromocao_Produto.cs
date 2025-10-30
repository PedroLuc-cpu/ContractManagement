using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProdutoIdOnTablePromocao_Produto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_promocao_produto_produtos_produto_id",
                table: "promocao_produto");

            migrationBuilder.RenameColumn(
                name: "produto_id",
                table: "promocao_produto",
                newName: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_promocao_produto_produtos_ProdutoId",
                table: "promocao_produto",
                column: "ProdutoId",
                principalTable: "produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_promocao_produto_produtos_ProdutoId",
                table: "promocao_produto");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "promocao_produto",
                newName: "produto_id");

            migrationBuilder.AddForeignKey(
                name: "FK_promocao_produto_produtos_produto_id",
                table: "promocao_produto",
                column: "produto_id",
                principalTable: "produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
