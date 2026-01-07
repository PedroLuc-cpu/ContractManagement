using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Pagamento.Command
{
    public sealed record ConfirmarPagamentoCommand(Guid IdPedido) : ICommand
    {
    }
}
