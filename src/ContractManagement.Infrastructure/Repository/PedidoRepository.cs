using ContractManagement.Domain.Entity.Pedido;
using ContractManagement.Infrastructure.Data;
using ContractManagement.Infrastructure.Repository.Contracts.IPedido;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Infrastructure.Repository
{
    public class PedidoRepository(ContractManagementContext context) : IPedidoRepository
    {
        private readonly ContractManagementContext _context = context;

        public async Task Atualizar(Pedido pedido)
        {
            await _context.Pedidos.Where(p => p.Id == pedido.Id).ExecuteUpdateAsync(set => set.SetProperty(b => b.ValorTotal, pedido.ValorTotal));            
        }

        public async Task Inserir(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
        }

        public async Task<List<Pedido>> Listar()
        {
            var listar = await _context.Pedidos.ToListAsync();
            if (listar.Count > 0)
            {
                return listar;
            }
            return listar;                   
        }

        public async Task<Pedido> ObterPorId(Guid id)
        {
            var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
            if (pedido != null)
            {
                return pedido;
            }
            return new Pedido();
            
        }

        public async Task Salvar()
        {
            await _context.SaveChangesAsync();
        }

    }
}
