using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefaturacaoDasTabelasDePedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "disponibilidade_produto");

            migrationBuilder.DropTable(
                name: "endereco");

            migrationBuilder.DropTable(
                name: "item_pedido");

            migrationBuilder.DropTable(
                name: "promocao_produto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pedido",
                table: "pedido");

            migrationBuilder.DropIndex(
                name: "IX_pedido_numero_pedido",
                table: "pedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_produtos",
                table: "produtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clientes",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "numero_pedido",
                table: "pedido");

            migrationBuilder.RenameTable(
                name: "pedido",
                newName: "Pedido");

            migrationBuilder.RenameTable(
                name: "produtos",
                newName: "Produto");

            migrationBuilder.RenameTable(
                name: "clientes",
                newName: "Cliente");

            migrationBuilder.RenameColumn(
                name: "id_cliente",
                table: "Pedido",
                newName: "IdCliente");

            migrationBuilder.RenameColumn(
                name: "dt_update",
                table: "Pedido",
                newName: "DataAtualizao");

            migrationBuilder.RenameColumn(
                name: "dt_created",
                table: "Pedido",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "valor_total",
                table: "Pedido",
                newName: "ValorTotal");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Pedido",
                newName: "StatusPedido");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Produto",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "codigo",
                table: "Produto",
                newName: "Codigo");

            migrationBuilder.RenameColumn(
                name: "ativo",
                table: "Produto",
                newName: "Ativo");

            migrationBuilder.RenameColumn(
                name: "und_medida",
                table: "Produto",
                newName: "UnidadeMedida");

            migrationBuilder.RenameColumn(
                name: "obersavao",
                table: "Produto",
                newName: "Observacao");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "Produto",
                newName: "Imagem");

            migrationBuilder.RenameColumn(
                name: "estoque_min",
                table: "Produto",
                newName: "EstoqueMinimo");

            migrationBuilder.RenameColumn(
                name: "estoque_max",
                table: "Produto",
                newName: "EstoqueMaximo");

            migrationBuilder.RenameColumn(
                name: "estoque_atual",
                table: "Produto",
                newName: "EstoqueAtual");

            migrationBuilder.RenameColumn(
                name: "cod_barras",
                table: "Produto",
                newName: "CodigoBarras");

            migrationBuilder.RenameColumn(
                name: "preco_venda",
                table: "Produto",
                newName: "PrecoVenda");

            migrationBuilder.RenameColumn(
                name: "preco_custo",
                table: "Produto",
                newName: "PrecoCusto");

            migrationBuilder.RenameColumn(
                name: "dt_update",
                table: "Cliente",
                newName: "DataAtualizao");

            migrationBuilder.RenameColumn(
                name: "dt_created",
                table: "Cliente",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "sobrenome",
                table: "Cliente",
                newName: "LastName_Value");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Cliente",
                newName: "FirstName_Value");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Cliente",
                newName: "Email_Value");

            migrationBuilder.RenameIndex(
                name: "IX_clientes_email",
                table: "Cliente",
                newName: "IX_Cliente_Email_Value");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorTotal",
                table: "Pedido",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "StatusPedido",
                table: "Pedido",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AddColumn<string>(
                name: "NumeroPedido",
                table: "Pedido",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedido",
                table: "Pedido",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Produto",
                table: "Produto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DisponibilidadeProduto",
                columns: table => new
                {
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Inicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Fim = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisponibilidadeProduto", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_DisponibilidadeProduto_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Endereco_Cliente",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rua = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Numero = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    Cidade = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Estado = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Cep = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco_Cliente", x => x.ClienteId);
                    table.ForeignKey(
                        name: "FK_Endereco_Cliente_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPedido",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdProduto = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeProduto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PrecoUnitario_Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PrecoUnitario_Coin = table.Column<string>(type: "text", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    PedidoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPedido_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromocaoProduto",
                columns: table => new
                {
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescontoPercentual = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Periodo_Inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Periodo_Fim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromocaoProduto", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_PromocaoProduto_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_NumeroPedido",
                table: "Pedido",
                column: "NumeroPedido",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_PedidoId",
                table: "ItemPedido",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisponibilidadeProduto");

            migrationBuilder.DropTable(
                name: "Endereco_Cliente");

            migrationBuilder.DropTable(
                name: "ItemPedido");

            migrationBuilder.DropTable(
                name: "PromocaoProduto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedido",
                table: "Pedido");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_NumeroPedido",
                table: "Pedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Produto",
                table: "Produto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "NumeroPedido",
                table: "Pedido");

            migrationBuilder.RenameTable(
                name: "Pedido",
                newName: "pedido");

            migrationBuilder.RenameTable(
                name: "Produto",
                newName: "produtos");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "clientes");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "pedido",
                newName: "id_cliente");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "pedido",
                newName: "dt_created");

            migrationBuilder.RenameColumn(
                name: "DataAtualizao",
                table: "pedido",
                newName: "dt_update");

            migrationBuilder.RenameColumn(
                name: "ValorTotal",
                table: "pedido",
                newName: "valor_total");

            migrationBuilder.RenameColumn(
                name: "StatusPedido",
                table: "pedido",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "produtos",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Codigo",
                table: "produtos",
                newName: "codigo");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "produtos",
                newName: "ativo");

            migrationBuilder.RenameColumn(
                name: "UnidadeMedida",
                table: "produtos",
                newName: "und_medida");

            migrationBuilder.RenameColumn(
                name: "Observacao",
                table: "produtos",
                newName: "obersavao");

            migrationBuilder.RenameColumn(
                name: "Imagem",
                table: "produtos",
                newName: "image");

            migrationBuilder.RenameColumn(
                name: "EstoqueMinimo",
                table: "produtos",
                newName: "estoque_min");

            migrationBuilder.RenameColumn(
                name: "EstoqueMaximo",
                table: "produtos",
                newName: "estoque_max");

            migrationBuilder.RenameColumn(
                name: "EstoqueAtual",
                table: "produtos",
                newName: "estoque_atual");

            migrationBuilder.RenameColumn(
                name: "CodigoBarras",
                table: "produtos",
                newName: "cod_barras");

            migrationBuilder.RenameColumn(
                name: "PrecoVenda",
                table: "produtos",
                newName: "preco_venda");

            migrationBuilder.RenameColumn(
                name: "PrecoCusto",
                table: "produtos",
                newName: "preco_custo");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "clientes",
                newName: "dt_created");

            migrationBuilder.RenameColumn(
                name: "DataAtualizao",
                table: "clientes",
                newName: "dt_update");

            migrationBuilder.RenameColumn(
                name: "LastName_Value",
                table: "clientes",
                newName: "sobrenome");

            migrationBuilder.RenameColumn(
                name: "FirstName_Value",
                table: "clientes",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Email_Value",
                table: "clientes",
                newName: "email");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_Email_Value",
                table: "clientes",
                newName: "IX_clientes_email");

            migrationBuilder.AlterColumn<decimal>(
                name: "valor_total",
                table: "pedido",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "pedido",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "numero_pedido",
                table: "pedido",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pedido",
                table: "pedido",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_produtos",
                table: "produtos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clientes",
                table: "clientes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "disponibilidade_produto",
                columns: table => new
                {
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    fim = table.Column<TimeSpan>(type: "interval", nullable: false),
                    inicio = table.Column<TimeSpan>(type: "interval", nullable: false)
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
                name: "endereco",
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
                    table.PrimaryKey("PK_endereco", x => x.ClienteId);
                    table.ForeignKey(
                        name: "FK_endereco_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "item_pedido",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_produto = table.Column<Guid>(type: "uuid", nullable: false),
                    PedidoId = table.Column<Guid>(type: "uuid", nullable: false),
                    produto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    quantidade = table.Column<int>(type: "integer", nullable: false),
                    PrecoUnitario_Coin = table.Column<string>(type: "text", nullable: false),
                    preco_unitario = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_item_pedido_pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "promocao_produto",
                columns: table => new
                {
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    desconto_percentual = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    fim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promocao_produto", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_promocao_produto_produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pedido_numero_pedido",
                table: "pedido",
                column: "numero_pedido",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_item_pedido_PedidoId",
                table: "item_pedido",
                column: "PedidoId");
        }
    }
}
