using ContractManagement.Domain.Entity.Clientes;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Interfaces.Repository.Clientes
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        Task<Cliente?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task CreateClient(Cliente cliente, CancellationToken cancellationToken = default);
    }
}
