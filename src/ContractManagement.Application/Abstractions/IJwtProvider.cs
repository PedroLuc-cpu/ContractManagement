using ContractManagement.Domain.Entity;

namespace ContractManagement.Application.Abstractions
{
    public interface IJwtProvider 
    {
        string Generete(Member member);
    }
}
