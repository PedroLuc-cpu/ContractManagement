using ContractManagement.Domain.DTO;
using ContractManagement.Domain.Entity.Clientes;
using ContractManagement.Domain.Primitives;
using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Domain.Interfaces.Repository.Clientes
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        Task<Cliente?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task CreateClientAsync(string nome, string sobrenone, string email, Endereco endereco, CancellationToken cancellationToken = default);
        Task<IEnumerable<Cliente>> GetAllClientsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task UpdateClientAsync(ClienteUpdateRequestDto cliente, CancellationToken cancellationToken = default);
    }
}
