using ContractManagement.Domain.Common.Base;

namespace ContractManagement.Domain.Entity.Endereco
{
    public class EnderecoEntity : ValueObject
    {
        public string Rua { get; private set; } = string.Empty;
        public string Numero { get; private set; } = string.Empty;
        public string Cidade { get; private set; } = string.Empty;
        public string Estado { get; private set; } = string.Empty;
        public string CEP { get; private set; } = string.Empty;

        public EnderecoEntity(string rua, string numero, string cidade, string estado, string cep)
        {
            Rua = rua;
            Numero = numero;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Rua;
            yield return Numero;
            yield return Cidade;
            yield return Estado;
            yield return CEP;
        }
    }
}
