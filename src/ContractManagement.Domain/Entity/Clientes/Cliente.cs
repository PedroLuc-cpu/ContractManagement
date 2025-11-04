using ContractManagement.Domain.Common.Validations;
using ContractManagement.Domain.Primitives;
using ContractManagement.Domain.Shared;
using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Domain.Entity.Clientes
{
    public class Cliente : AggregateRoot
    {
        protected Cliente(): base(id: Guid.Empty, dataCriacao: DateTime.UtcNow) { }
        private Cliente(FirstName nome, LastName lastName, Email email, Endereco? endereco) : base(Guid.NewGuid(), dataCriacao: DateTime.UtcNow) {
            FirstName = nome;
            LastName = lastName;
            Email = email;
            Endereco = endereco;
        }

        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public Email Email { get; private set; }
        public Endereco? Endereco { get; private set; }

        public static Cliente Create(string firstName, string lastName, string email, string street, string number, string city, string state, string zipCode)
        {
            var cliente = new Cliente(FirstName.Create(firstName).Value, LastName.Create(lastName).Value, Email.Create(email).Value, Endereco.Create(street, number, city, state, zipCode).Value);
            return cliente;             
        }
        public void Update(string firstName, string lastName, string email)
        {
            Guard.AgaintNull(firstName, nameof(firstName));
            Guard.AgaintNull(lastName,nameof(lastName));
            Guard.AgaintNull(email, nameof(email));

            FirstName = FirstName.Create(firstName).Value;
            LastName = LastName.Create(lastName).Value;
            Email = Email.Create(email).Value;
            
            SetDataAtualizacao();

        }
        public void UpdateAddress(string street, string number, string city, string state, string zipCode)
        {
            Endereco = Endereco.Create(street, number, city, state, zipCode).Value;

            SetDataAtualizacao();
        }
        public void RemoverEndereco()
        {
            Endereco = null;
            SetDataAtualizacao();
        }
    }
}
