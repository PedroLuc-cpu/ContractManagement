using ContractManagement.Domain.Common.Exceptions;
using ContractManagement.Domain.Common.Validations;
using ContractManagement.Domain.Primitives;
using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Domain.Entity.Catalogo
{
    public class Produto : AggregateRoot
    {
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }
        public string Observacao { get; private set; }
        public string CodigoBarras { get; private set; }
        public string Codigo { get; private set; }
        public string UnidadeMedida { get; private set; }
        public Money PrecoVenda { get; private set; }
        public Money PrecoCusto { get; private set; }
        public int EstoqueAtual { get; private set; }
        public int EstoqueMinimo { get; private set; }
        public int EstoqueMaximo { get; private set; }
        public Promocao? Promocao { get; private set; }
        public HorarioDisponibilidade? Disponibilidade { get; private set; }

        protected Produto(): base(id: Guid.Empty, dataCriacao: DateTime.UtcNow) { }
        private Produto(string nome,
            string unidadeMedida,
            string codigoBarras,
            string observacao,
            string codigo,
            Money precoVenda,
            Money precoCusto,
            int estoqueAtual,
            int estoqueMinino,
            int estoqueMaximo,
            bool ativo
            ): base(Guid.NewGuid(), dataCriacao: DateTime.UtcNow)
        {
            Nome = nome;
            Observacao = observacao;
            Ativo = ativo;
            CodigoBarras = codigoBarras;
            Codigo = codigo;
            UnidadeMedida = unidadeMedida;
            PrecoVenda = precoVenda;
            PrecoCusto = precoCusto;
            EstoqueAtual = estoqueAtual;
            EstoqueMinimo = estoqueMinino;
            EstoqueMaximo = estoqueMaximo;
            Disponibilidade = null;

        }

        public void Limpar()
        {
            Nome = string.Empty;
            Observacao = string.Empty;
            CodigoBarras = string.Empty;
            Codigo = string.Empty;
            UnidadeMedida = string.Empty;
            PrecoVenda = Money.Create(0).Value;
            PrecoCusto = Money.Create(0).Value;
            EstoqueAtual = 0;
            EstoqueMinimo = 0;
            EstoqueMaximo = 0;
            Ativo = false;
        }

        public static Produto Create(
            string nome,
            string unidadeMedida,
            string codigoBarras,
            string observacao,
            string codigo,
            decimal precoVenda,
            decimal precoCusto,
            int estoqueAtual,
            int estoqueMinino,
            int estoqueMaximo,
            bool ativo
            )
        {
            Guard.AgaintNull(nome, nameof(nome));
            Guard.AgaintNull(codigo, nameof(codigo));
            Guard.AgaintNull(precoVenda, nameof(precoVenda));
            Guard.AgaintNull(precoCusto, nameof(precoCusto));
            Guard.AgaintNull(estoqueAtual, nameof(precoCusto));
            Guard.AgaintNull(estoqueMaximo, nameof(precoCusto));
            Guard.AgaintNull(estoqueMinino, nameof(precoCusto));
            Guard.AgaintNull(unidadeMedida, nameof(precoCusto));
            Guard.AgaintNull(observacao, nameof(precoCusto));
            Guard.Againts<DomainException>(precoVenda <= 0, "O preço de venda não pode zero ou negativo");
            Guard.Againts<DomainException>(precoCusto < 0, "O preço de custo não pode ser negativo");          
            Guard.Againts<DomainException>(precoCusto > precoVenda, "O preço de custo não pode ser maior que o preço de venda");

            var produto = new Produto(
                nome,
                unidadeMedida,
                codigoBarras,
                observacao,
                codigo,
                Money.Create(precoVenda).Value,
                Money.Create(precoCusto).Value,
                estoqueAtual,
                estoqueMinino,
                estoqueMaximo,
                ativo
            );

            return produto;
         
        }
        public decimal PrecoFinal => Promocao is not null && Promocao.IsActive() ? PrecoVenda.Value - (PrecoVenda.Value * Promocao.DescontoPercentual / 100) : PrecoVenda.Value;
        public bool IsAvailableNow() => Ativo && (Disponibilidade is null || Disponibilidade.IsAvailableNow());
        public void DefinirDisponibilidade(HorarioDisponibilidade disponibilidade)
        {
            Disponibilidade = disponibilidade;
        }

        public void AtualizarProduto(string nome, string unidadeMedida, string codigoBarras, string observacao, bool ativo)
        {
            Nome = nome;
            UnidadeMedida = unidadeMedida;
            CodigoBarras = codigoBarras;
            Observacao = observacao;
            Ativo = ativo;
            SetDataAtualizacao();
        }

        public void AdicionarPromocao(Promocao promocao)
        {
            Promocao = promocao;
        }
        public void RemoverPromocao() => Promocao = null;
        public void AtualizarEstoque(int novaQuantidade)
        {
            Guard.Againts<DomainException>(novaQuantidade < 0, "Quantidade inválida");
            EstoqueAtual = novaQuantidade;
        }
        public void AtualizarPrecoVenda(Money novoPreco)
        {
            Guard.Againts<DomainException>(novoPreco.Value <= 0, "O preço de venda não pode ser zero ou negativo");
            PrecoVenda = novoPreco;
        }
        public void AtualizarPrecoCusto(Money novoPreco)
        {
            Guard.Againts<DomainException>(novoPreco.Value < 0, "O preço de custo não pode ser negativo");
            PrecoCusto = novoPreco;
        }
    }
}
