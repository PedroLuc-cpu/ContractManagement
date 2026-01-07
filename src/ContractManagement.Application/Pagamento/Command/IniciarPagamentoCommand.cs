using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Pagamento.Command
{
    public sealed record IniciarPagamentoCommand(Guid IdPedido) : ICommand<string>
    {
    }
}
