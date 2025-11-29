using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoNovasColunasNoUsuarioEntradaSaidaStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endereco_Cep",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "Endereco_Cidade",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "Endereco_Estado",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "Endereco_Numero",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "Endereco_Rua",
                table: "clientes");

            migrationBuilder.RenameColumn(
                name: "NomeComplity",
                table: "AspNetUsers",
                newName: "DescricaoStatus");

            migrationBuilder.AddColumn<DateTime>(
                name: "Entrada",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Saida",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StatusUser",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "endereco",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    rua = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    numero = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    cidade = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    estado = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    cep = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_endereco", x => x.ClienteId);
                    table.ForeignKey(
                        name: "FK_endereco_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "endereco");

            migrationBuilder.DropColumn(
                name: "Entrada",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Saida",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StatusUser",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DescricaoStatus",
                table: "AspNetUsers",
                newName: "NomeComplity");

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Cep",
                table: "clientes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Cidade",
                table: "clientes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Estado",
                table: "clientes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Numero",
                table: "clientes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Rua",
                table: "clientes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
