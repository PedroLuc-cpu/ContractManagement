using ContractManagement.Domain.Common.Base;
using ContractManagement.Domain.Entity.Enderecos;
using ContractManagement.Domain.Primitives;
using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Domain.Entity.Clientes
{
    public class Cliente : EntityBase, IAggregateRoot
    {
        public Cliente(FirstName nome, LastName lastName, Email email, Endereco? endereco = null)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            LastName = lastName;
            Email = email;
            Endereco = endereco;
        }
        private Cliente() { }

        public FirstName Nome { get; private set; }
        public LastName LastName { get; private set; }
        public Email Email { get; private set; }
        public Endereco? Endereco { get; private set; }

        public void MudarEndereco(Endereco endereco)
        {
            Endereco = endereco;
            SetDataAtualizacao();
        }



    }
}
