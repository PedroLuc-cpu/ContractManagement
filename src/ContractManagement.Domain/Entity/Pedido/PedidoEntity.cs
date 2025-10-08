using ContractManagement.Domain.Common.Base;

namespace ContractManagement.Domain.Entity.Pedido
{
    public class PedidoEntity : EntityBase
    {
        public decimal ValorTotal { get; private set; }

        public PedidoEntity() 
        {
            Id = Guid.NewGuid();
        }
        public void AdicionarItem(decimal valor)
        {
            ValorTotal += valor;
            SetDataAtualizacao();
        }
    }
}
