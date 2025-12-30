using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity;
using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.Shared;
using ContractManagement.Domain.ValueObjects;


namespace ContractManagement.Application.Members.CreateMember;

internal sealed class CreateMemberCommandHandler(
    IMemberRepository memberRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateMemberCommand>
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        var firstNameResult = FirstName.Create(request.FirstName);
        var lastNameResult = LastName.Create(request.LastName);

        var member = new Member(
            Guid.NewGuid(),
            emailResult.Value,
            firstNameResult.Value,
            lastNameResult.Value);

        _memberRepository.Add(member);

        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}