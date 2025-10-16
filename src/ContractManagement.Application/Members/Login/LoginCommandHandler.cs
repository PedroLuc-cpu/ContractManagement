using ContractManagement.Application.Abstractions;
using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity;
using ContractManagement.Domain.Erros;
using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.Shared;
using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Application.Members.Login
{
    internal sealed class LoginCommandHandler(IMemberRepository memberRepository, IJwtProvider jwtProvider) : ICommandHandler<LoginCommand, string>
    {
        private readonly IMemberRepository _memberRepository = memberRepository;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            Result<Email> email = Email.Create(request.Email);

            Member? member =  await _memberRepository.GetByEmailAsync(email.Value, cancellationToken);

            if (member is null)
            {
                return Result.Failure<string>(DomainErrors.Member.InvalidCredentials);
            }

            string token = _jwtProvider.Generete(member);
            return token;            
        }
    }
}
