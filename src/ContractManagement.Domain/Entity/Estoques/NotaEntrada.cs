using ContractManagement.Domain.Common.Exceptions;
using ContractManagement.Domain.Common.Validations;
using ContractManagement.Domain.Events;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Entity.Estoques
{
    public class NotaEntrada : AggregateRoot
    {
        public string NumeroDocumento { get; private set; } = string.Empty;
        public DateTime DataEntrada { get; private set; }

        private readonly List<ItemNotaEntrada> _itens = [];
        public IReadOnlyCollection<ItemNotaEntrada> Itens => _itens.AsReadOnly();

        protected NotaEntrada() : base(Guid.Empty, DateTime.UtcNow) { }

        public NotaEntrada(string numeroDocumento, DateTime dataEntrada) : base(Guid.NewGuid(), DateTime.UtcNow)
        {
            NumeroDocumento = numeroDocumento;
            DataEntrada = dataEntrada;
        }

        public static NotaEntrada Create(string numeroDocumento, IReadOnlyCollection<ItemNotaEntradaData> itens)
        {
            Guard.Againts<DomainException>(string.IsNullOrWhiteSpace(numeroDocumento), "Número do documento é obrigatório.");
            Guard.Againts<DomainException>(itens == null || itens.Count == 0, "A nota de entrada deve conter ao menos um item.");

            var notaEntrada = new NotaEntrada(numeroDocumento, DateTime.UtcNow);

            foreach (var item in itens)
            {
                notaEntrada._itens.Add(new ItemNotaEntrada(item));
            }

            notaEntrada.RaiseDomainEvent(new NotaEntradaCreatedEvent(notaEntrada.Id, [.. notaEntrada.Itens.Select(i => new NotaEntradaItemSnapshot(i.IdProduto, i.Quantidade, i.PrecoUnitario))]));

            return notaEntrada;
        }



    }
}
