using ContractManagement.Domain.Common.Base;
using ContractManagement.Domain.Common.Validations;

namespace ContractManagement.Domain.Entity.Catalogo
{
     public class Categoria : EntityBase
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        private readonly List<Produto> _Produtos = new();
        public IReadOnlyCollection<Produto> Produtos => _Produtos.AsReadOnly();
        private Categoria() { }
        public Categoria(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }
        public void AdicionarProduto(Produto produto)
        {
            Guard.AgaintNull(produto, nameof(produto));
            _Produtos.Add(produto);
            SetDataAtualizacao();
        }

        public void Atualizar(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
            SetDataAtualizacao();
        }
    }
}
