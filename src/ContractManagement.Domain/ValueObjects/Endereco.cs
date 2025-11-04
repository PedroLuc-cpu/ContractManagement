using ContractManagement.Domain.Common.Base;
using ContractManagement.Domain.Shared;
using System.Text.RegularExpressions;

namespace ContractManagement.Domain.ValueObjects
{
    public class Endereco : ValueObject
    {
        public string Rua { get; private set; }
        public string Numero { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Cep { get; private set; }

        private Endereco(string rua, string numero, string cidade, string estado, string cep)
        {
            Rua = rua;
            Numero = numero;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
        }

        public static Result<Endereco> Create(string rua, string numero, string cidade, string estado, string cep)
        {
            if (string.IsNullOrEmpty(rua))
            {
                return Result.Failure<Endereco>(new Error("street.EmptyOrNull", "A rua do endereço não pode ser vazio ou nulo"));

            }
            if (string.IsNullOrEmpty(numero))
            {
                return Result.Failure<Endereco>(new Error("number.EmptyOrNull", "O numero do endereço não pode ser vazio ou nulo"));
            }
            if (string.IsNullOrEmpty(cidade))
            {
                return Result.Failure<Endereco>(new Error("city.EmptyOrNull", "A cidade não pode ser vazio ou nulo."));
            }
            if (string.IsNullOrEmpty(estado))
            {
                return Result.Failure<Endereco>(new Error("state.EmptyOrNull", "O estado não pode ser vazio ou nulo"));
            }
            if (string.IsNullOrEmpty(cep) && cep.Length < 8 || cep.Length > 8)
            {
                return Result.Failure<Endereco>(new Error("zipCode.EmptyOrNull", "O Cep não pode ser vazio ou nulo"));
            }

            return new Endereco(rua, numero, cidade, estado, cep);
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
