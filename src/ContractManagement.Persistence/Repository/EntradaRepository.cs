using ContractManagement.Domain.Entity.Estoques;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Persistence.Repository
{
    public class EntradaRepository(ContractManagementContext context) : INotaEntradaRepository
    {
        private readonly ContractManagementContext _context = context;
        public async Task Adicionar(NotaEntrada notaEntrada, CancellationToken cancellationToken)
        {
           await _context.NotaEntradas.AddAsync(notaEntrada, cancellationToken);
        }

        public async Task<NotaEntrada?> ObterNotaEntradaPorNumeroDocumento(string NumeroDocumento, CancellationToken cancellationToken)
        {
            var notaEntrada = await _context.NotaEntradas.Include(i => i.Itens).FirstOrDefaultAsync(notaEntrada => notaEntrada.NumeroDocumento == NumeroDocumento, cancellationToken);
            return notaEntrada;
        }
    }
}
