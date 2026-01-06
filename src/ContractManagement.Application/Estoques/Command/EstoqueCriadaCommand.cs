using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Estoques.Command
{
    public sealed record EstoqueCriadaCommand(Guid IdProduto, int QuantidadeInicial) : ICommand
    {
    }
}
