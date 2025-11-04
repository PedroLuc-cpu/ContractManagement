using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Client.Command
{
    public sealed record CreateClientCommand(string FirstName, string LastName, string Email, string Street, string Number, string City, string State, string ZipCode) : ICommand
    {

    }
}
