using ContractManagement.Domain.Common.Base;
using ContractManagement.Domain.Common.Exceptions;
using ContractManagement.Domain.Common.Validations;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Entity
{
    public class Produto : EntityBase, IAggregateRoot
    {
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }
        public string Observacao { get; private set; }
        public string CodigoBarras { get; private set; }
        public string Codigo { get; private set; }
        public string UnidadeMedida { get; private set; }
        public decimal PrecoVenda { get; private set; }
        public decimal PrecoCusto { get; private set; }
        public int EstoqueAtual { get; private set; }
        public int EstoqueMinimo { get; private set; }
        public int EstoqueMaximo { get; private set; }

        private Produto() { }

        public Produto(
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

            Id = Guid.NewGuid();
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
        }
        public void AtualizarEstoque(int novaQuantidade)
        {
            Guard.Againts<DomainException>(novaQuantidade < 0, "Quantidade inválida");
            EstoqueAtual = novaQuantidade;
        }
        public void AtualizarPrecoVenda(decimal novoPreco)
        {
            Guard.Againts<DomainException>(novoPreco <= 0, "O preço de venda não pode ser zero ou negativo");
            PrecoVenda = novoPreco;
        }
        public void AtualizarPrecoCusto(decimal novoPreco)
        {
            Guard.Againts<DomainException>(novoPreco < 0, "O preço de custo não pode ser negativo");
            PrecoCusto = novoPreco;
        }
    }
}
