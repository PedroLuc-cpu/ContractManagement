using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Presentation.Model
{
    public sealed record ClienteRequest(
        string Nome,
        string Sobrenome,
        string Email,
        AddressRequest Endereco
    )
    {
    }
    public sealed record ClienteUpdateRequest(
        Guid Id,
        string Nome,
        string Sobrenome,
        string Email,
        AddressRequest Endereco
    )
    { 
    }
    public sealed record AddressRequest(string Rua, string Numero, string Cidade, string Estado, string Cep)
    {
    }

    public sealed record ClienteResponse(
       Guid Id,
       string Nome,
       string Sobrenome,
       string Email,
       AddressRequest Endereco) 
    { 
    }
}
