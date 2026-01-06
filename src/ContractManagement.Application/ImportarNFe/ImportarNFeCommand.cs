using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.ImportarNFe
{
    public sealed record ImportarNFeCommand(string XmlContent) : ICommand<Guid>
    {
    }
}
