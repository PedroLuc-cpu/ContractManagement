using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.ValueObjects
{
    public class HorarioDisponibilidade : ValueObject
    {
        public TimeSpan Inicio { get; private set; }
        public TimeSpan Fim {  get; private set; }
        private HorarioDisponibilidade() {}
        
        public bool IsAvailableNow()
        {
            var now = DateTime.Now.TimeOfDay;
            return now >= Inicio && now <= Fim;
        }
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Inicio;
            yield return Fim;
        }
    }
}
