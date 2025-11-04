using ContractManagement.Domain.Entity.Clientes;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Interfaces.Repository.Clientes
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        Task<Cliente?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task CreateClientAsync(Cliente cliente, CancellationToken cancellationToken = default);
        Task<IEnumerable<Cliente>> GetAllClientsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task UpdateClientAsync(Cliente cliente, CancellationToken cancellationToken = default);
    }
}
