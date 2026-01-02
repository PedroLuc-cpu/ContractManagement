using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.ValueObjects
{
    public sealed class NumeroPedido : ValueObject
    {
        public string Value { get; } = string.Empty;

        private NumeroPedido(string value)
        {

            Value = value;
        }

        public static NumeroPedido Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("O número do pedido não pode ser vazio.", nameof(value));
            }
            return new NumeroPedido(value);
        }

        public override string ToString() => Value;
        public override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
