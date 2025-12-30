using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableSolicitacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Solicitacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdFuncionario = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolitacaoHistorico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSolicitacao = table.Column<Guid>(type: "uuid", nullable: false),
                    Acao = table.Column<string>(type: "text", nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: true),
                    OcorridoEm = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolitacaoHistorico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolitacaoHistorico_Solicitacoes_IdSolicitacao",
                        column: x => x.IdSolicitacao,
                        principalTable: "Solicitacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolitacaoHistorico_IdSolicitacao",
                table: "SolitacaoHistorico",
                column: "IdSolicitacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolitacaoHistorico");

            migrationBuilder.DropTable(
                name: "Solicitacoes");
        }
    }
}
