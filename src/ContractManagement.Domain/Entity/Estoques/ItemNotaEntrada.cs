using ContractManagement.Domain.Common.Exceptions;
using ContractManagement.Domain.Common.Validations;

namespace ContractManagement.Domain.Entity.Estoques
{
    public class ItemNotaEntrada
    {
        public Guid Id { get; private set; }
        public Guid IdProduto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        protected ItemNotaEntrada() { }

        public static ItemNotaEntrada Create(ItemNotaEntradaData itemNotaEntrada)
        {
            return new ItemNotaEntrada(itemNotaEntrada);
        }

        internal ItemNotaEntrada(ItemNotaEntradaData itemNotaEntrada)
        {
            Guard.AgainstEmptyGuid(itemNotaEntrada.IdProduto, nameof(itemNotaEntrada.IdProduto));
            Guard.Againts<DomainException>(itemNotaEntrada.Quantidade <= 0, "Quantidade deve ser maior que zero.");
            Guard.Againts<DomainException>(itemNotaEntrada.PrecoUnitario <= 0, "Preço unitário deve ser maior que zero.");

            Id = Guid.NewGuid();
            IdProduto = itemNotaEntrada.IdProduto;
            Quantidade = itemNotaEntrada.Quantidade;
            PrecoUnitario = itemNotaEntrada.PrecoUnitario;

        }

    }
}
