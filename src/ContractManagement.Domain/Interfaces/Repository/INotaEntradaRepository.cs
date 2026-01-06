using ContractManagement.Domain.Entity.Estoques;

namespace ContractManagement.Domain.Interfaces.Repository
{
    public interface INotaEntradaRepository
    {
        Task Adicionar(NotaEntrada notaEntrada, CancellationToken cancellationToken);
        Task<NotaEntrada?> ObterNotaEntradaPorNumeroDocumento(string NumeroDocumento, CancellationToken cancellationToken);
    }
}
