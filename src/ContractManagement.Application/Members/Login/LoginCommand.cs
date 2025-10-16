using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Members.Login
{
    public record LoginCommand(string Email) : ICommand<string>
    {
    }
}
