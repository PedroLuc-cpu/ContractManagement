using ContractManagement.Domain.Common.Base;

namespace ContractManagement.Domain.ValueObjects
{
    public class Endereco(string rua, string numero, string cidade, string estado, string cep) : ValueObject
    {
        public string Rua { get; private set; } = rua;
        public string Numero { get; private set; } = numero;
        public string Cidade { get; private set; } = cidade;
        public string Estado { get; private set; } = estado;
        public string Cep { get; private set; } = cep;

        private Endereco() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
        {
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Rua;
            yield return Numero;
            yield return Cidade;
            yield return Estado;
            yield return Cep;
        }
    }
}
