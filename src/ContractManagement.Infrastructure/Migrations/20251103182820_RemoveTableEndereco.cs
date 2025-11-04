using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTableEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "enderecos");

            migrationBuilder.AddColumn<string>(
                name: "cep",
                table: "clientes",
                type: "character varying(8)",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cidade",
                table: "clientes",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "estado",
                table: "clientes",
                type: "character varying(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "numero",
                table: "clientes",
                type: "character varying(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "rua",
                table: "clientes",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cep",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "cidade",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "numero",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "rua",
                table: "clientes");

            migrationBuilder.CreateTable(
                name: "enderecos",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    cep = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    cidade = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    estado = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    numero = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    rua = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enderecos", x => x.ClienteId);
                    table.ForeignKey(
                        name: "FK_enderecos_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
