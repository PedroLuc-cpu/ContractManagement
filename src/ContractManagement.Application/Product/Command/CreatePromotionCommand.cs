using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Product.Command
{
    public sealed record CreatePromotionCommand(Guid ProdutoId, decimal DescontoPercentual, PeriodoPromocionalDTO Periodo) : ICommand
    {
    }
    public sealed record PeriodoPromocionalDTO(DateTime Inicio, DateTime Fim) : ICommand;
}
