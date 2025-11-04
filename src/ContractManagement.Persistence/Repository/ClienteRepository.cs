using ContractManagement.Domain.Entity.Clientes;
using ContractManagement.Domain.Interfaces.Repository.Clientes;
using ContractManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Persistence.Repository
{
    public class ClienteRepository(ContractManagementContext context) : BaseRepository<Cliente>(context), IClienteRepository
    {
        public async Task CreateClientAsync(Cliente cliente, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(cliente, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

        }

        public async Task<IEnumerable<Cliente>> GetAllClientsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var clients = await _dbSet.AsNoTracking()
                .OrderBy(c => c.FirstName.Value)
                .OrderByDescending(c => c.DataCriacao)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return clients;
        }

        public async Task<Cliente?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var cliente = await _dbSet.Where(c => c.Email.Value == email).FirstOrDefaultAsync(cancellationToken);
            return cliente;            
        }

        public async Task UpdateClientAsync(Cliente cliente, CancellationToken cancellationToken = default)
        {

            await _dbSet.ExecuteUpdateAsync(set => set
                .SetProperty(c => c.FirstName.Value, cliente.FirstName.Value)
                .SetProperty(c => c.LastName.Value, cliente.LastName.Value)
                .SetProperty(c => c.Email.Value, cliente.Email.Value)
                .SetProperty(c => c.Endereco.Rua, cliente.Endereco.Rua)
                .SetProperty(c => c.Endereco.Numero, cliente.Endereco.Rua)
                .SetProperty(c => c.Endereco.Cidade, cliente.Endereco.Cidade)
                .SetProperty(c => c.Endereco.Estado, cliente.Endereco.Estado)
                .SetProperty(c => c.Endereco.Cep, cliente.Endereco.Cep)
                , cancellationToken);
        }
    }
}
