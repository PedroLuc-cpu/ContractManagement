using ContractManagement.Domain.Entity.Clientes;
using ContractManagement.Domain.Interfaces.Repository.Clientes;
using ContractManagement.Infrastructure.Persistence;

namespace ContractManagement.Persistence.Repository
{
    public class ClienteRepository(ContractManagementContext context) : BaseRepository<Cliente>(context), IClienteRepository
    {
        public Task CreateClient(Cliente cliente, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Cliente?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
