namespace ContractManagement.Domain.Entity.Pedido
{
    public class Pedido
    {
        public Guid Id { get;  private set; }
        public decimal ValorTotal { get; private set; }

        public Pedido() 
        {
            Id = Guid.NewGuid();
        }
        public void AdicionarItem(decimal valor)
        {
            ValorTotal += valor;
        }
    }
}
