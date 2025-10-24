using ContractManagement.Domain.Common.Base;
using ContractManagement.Domain.Common.Validations;
using ContractManagement.Domain.Entity.Enderecos;
using ContractManagement.Domain.Erros;
using ContractManagement.Domain.Primitives;
using ContractManagement.Domain.Shared;
using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Domain.Entity.Clientes
{
    public class Cliente : EntityBase, IAggregateRoot
    {
        public Cliente(FirstName nome, LastName lastName, Email email, Endereco? endereco)
        {
            Guard.AgaintNull(nome, nameof(nome));
            Guard.AgaintNull(lastName, nameof(lastName));   
            Guard.AgaintNull(email, nameof(email)); 
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
        public void RemoverEndereco()
        {
            Endereco = null;
            SetDataAtualizacao();
        }
        public static Result<Cliente> AtualizarCliente(FirstName firstName, LastName lastName, Email email)
        {
            if (string.IsNullOrEmpty(firstName.Value))
            {
                return Result.Failure<Cliente>(DomainErrors.FirstName.Empty);
            }
            if (string.IsNullOrEmpty(lastName.Value))
            {
                return Result.Failure<Cliente>(DomainErrors.LastName.Empty);
            }
            if (string.IsNullOrEmpty(email.Value))
            {
                return Result.Failure<Cliente>(DomainErrors.Email.Empty);
            }
            var cliente = new Cliente
            {
                Nome = firstName,
                LastName = lastName,
                Email = email
            };
            return Result.Success(cliente);
        }
    }
}
