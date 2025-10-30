using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Domain.DTO
{
    public sealed record ClienteRequestDto(
        string Nome,
        string Sobrenome,
        string Email,
        Endereco Endereco
    )
    {
    }
    public sealed record ClienteUpdateRequestDto(
        FirstName FirstName,
        LastName LastName,
        Email Email,
        Endereco Endereco
    )
    {
        public sealed record ClienteResponseDto(
        Guid Id,
        string Nome,
        string Sobrenome,
        string Email,
        Endereco Endereco
    )
        {
        }
    }
}
