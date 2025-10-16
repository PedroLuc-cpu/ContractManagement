using ContractManagement.Application.Abstractions.Messaging;
namespace ContractManagement.Application.Members.CreateMember
{
    public sealed record CreateMemberCommand(string Email, string FirstName, string LastName) : ICommand;
    
}
