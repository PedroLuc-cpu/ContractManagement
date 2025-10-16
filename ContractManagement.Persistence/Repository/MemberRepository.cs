using ContractManagement.Domain.Entity;
using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Persistence.Repository
{
    internal sealed class MemberRepository : IMemberRepository
    {
        public void Add(Member member)
        {
        }

        public async Task<Member?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            return null;
        }

        public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            return null;
        }
    }
}
