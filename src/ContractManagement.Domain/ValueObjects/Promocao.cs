using ContractManagement.Domain.Common.Exceptions;
using ContractManagement.Domain.Common.Validations;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.ValueObjects
{
    public class Promocao : ValueObject
    {
        public decimal DescontoPercentual { get; private set; }
        public PeriodoPromocional Periodo { get; private set; }

        public void Limpar()
        {
            DescontoPercentual = 0;
            Periodo = new PeriodoPromocional(DateTime.MinValue, DateTime.MinValue);
        }

        private Promocao() { }
        public Promocao(decimal descontoPercentual, PeriodoPromocional periodo)
        {
            Guard.Againts<DomainException>(descontoPercentual <= 0 || descontoPercentual > 100, "O desconto deve estar entre 0 e 100%");
            DescontoPercentual = descontoPercentual;
            Periodo = periodo;
        }
        public bool IsActive() => Periodo.IsActive();

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Periodo;
            yield return DescontoPercentual;
        }
    }
}
