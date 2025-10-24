using ContractManagement.Domain.Entity.Enderecos;

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
        string Nome,
        string Sobrenome,
        string Email
    //Endereco Endereco
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
