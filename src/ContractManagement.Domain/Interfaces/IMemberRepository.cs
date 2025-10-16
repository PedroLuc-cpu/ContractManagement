using ContractManagement.Domain.Entity;
using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Domain.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Member?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);

        void Add(Member member);
    }
}
