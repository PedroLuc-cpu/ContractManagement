using ContractManagement.Domain.Erros;
using ContractManagement.Domain.Primitives;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Domain.ValueObjects
{
    public class Money : ValueObject
    {
        public decimal Value { get; private set; }
        public string Coin { get; private set; }
        private Money(decimal value, string coin = "BRL")
        {
            Value = value;
            Coin = coin;
        }

        public static Result<Money> Create(decimal value)
        {
            if (value < 0)
            {
                return Result.Failure<Money>(DomainErrors.Money.NegativeValue);
            }
            return new Money(value);

        }

        public override string ToString()
        {
            return $"{Coin} {Value:N2}";
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
            yield return Coin;
        }
    }
}
