using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.ValueObjects
{
    public sealed class OrderStatus : ValueObject
    {
        public static readonly OrderStatus Pendente = new("Pendente");
        public static readonly OrderStatus Aprovado = new("Aprovado");
        public static readonly OrderStatus Rejeitado = new("Rejeitado");
        public static readonly OrderStatus Cancelado = new("Cancelado");
        public static readonly OrderStatus Concluido = new("Concluido");

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Status;
        }

        public string Status { get; } = string.Empty;
        private OrderStatus(string status)
        {
            Status = status;
        }
        public override string ToString()
        {
            return Status;
        }

        // Definir transições válidas entre status
        public bool CanTransitionTo(OrderStatus newStatus)
        {
            if (this == Pendente && (newStatus == Aprovado || newStatus == Rejeitado))
                return true;
            if (this == Aprovado && newStatus == Rejeitado)
                return true;
            return false;
        }
    }
}
