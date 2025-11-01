using ContractManagement.Domain.Common.Base;
using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Domain.Entity
{
    public sealed class Member(Guid id, Email email, FirstName firstName, LastName lastName) : EntityBase(id, dataCriacao: DateTime.UtcNow)
    {
        public Email Email { get; set; } = email;
        public FirstName FirstName { get; set; } = firstName;
        public LastName LastName { get; set; } = lastName;
    }
}
