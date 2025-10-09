using ContractManagement.Domain.Common.Base;
using ContractManagement.Domain.Entity.Endereco;

namespace ContractManagement.Domain.Entity.Cliente
{
    public class ClienteEntity : EntityBase
    {
        public string Nome { get; private set; } = string.Empty;
        public EnderecoEntity? Endereco { get; private set; }

        public void MudarEndereco(EnderecoEntity endereco)
        {
            Endereco = endereco;
            SetDataAtualizacao();
        }
    }
}
