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
    }
}
