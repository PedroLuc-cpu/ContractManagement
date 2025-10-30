using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnNumeroAndStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "pedido",
                newName: "numero_pedido");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "pedido",
                newName: "id_cliente");

            migrationBuilder.RenameIndex(
                name: "IX_pedido_Numero",
                table: "pedido",
                newName: "IX_pedido_numero_pedido");

            migrationBuilder.AlterColumn<string>(
                name: "numero_pedido",
                table: "pedido",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "pedido",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "pedido");

            migrationBuilder.RenameColumn(
                name: "numero_pedido",
                table: "pedido",
                newName: "Numero");

            migrationBuilder.RenameColumn(
                name: "id_cliente",
                table: "pedido",
                newName: "IdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_pedido_numero_pedido",
                table: "pedido",
                newName: "IX_pedido_Numero");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "pedido",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);
        }
    }
}
