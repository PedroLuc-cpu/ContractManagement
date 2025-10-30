using ContractManagement.Domain.DTO;
using ContractManagement.Domain.Entity.Clientes;
using ContractManagement.Domain.Interfaces.Repository.Clientes;
using ContractManagement.Domain.ValueObjects;
using ContractManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Persistence.Repository
{
    public class ClienteRepository(ContractManagementContext context) : BaseRepository<Cliente>(context), IClienteRepository
    {
        public async Task CreateClientAsync(string nome, string sobrenone, string email, Endereco endereco, CancellationToken cancellationToken = default)
        {
            var nomeResult = FirstName.Create(nome);
            if (nomeResult.IsFailure)
            {
                throw new ArgumentException(nomeResult.Error);
            }
            var sobrenomeResult = LastName.Create(sobrenone);
            if (sobrenomeResult.IsFailure)
            {
                throw new ArgumentException(sobrenomeResult.Error);
            }
            var emailResult = Email.Create(email);
            if (emailResult.IsFailure) {
                throw new ArgumentException(emailResult.Error);
            }

            var cliente = Cliente.Create(nomeResult.Value, sobrenomeResult.Value, emailResult.Value, endereco);
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

        public async Task UpdateClientAsync(ClienteUpdateRequestDto cliente, CancellationToken cancellationToken = default)
        {
            var cs = Cliente.AtualizarCliente(cliente.FirstName, cliente.LastName, cliente.Email, cliente.Endereco);

            await _dbSet.ExecuteUpdateAsync(set => set
                .SetProperty(c => c, cs.Value), cancellationToken);
        }
    }
}
