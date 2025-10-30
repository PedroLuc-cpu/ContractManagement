using System.ComponentModel;

namespace ContractManagement.Domain.Enums
{
    public enum StatusPedidoEnum : byte
    {
        [Description("Pendente")]
        Pendente = 1,
        [Description("Aprovado")]
        Aprovado = 2,
        [Description("Rejeitado")]
        Rejeitado = 3
    }
    public static class StatusPedidoEnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            return value switch {
                StatusPedidoEnum.Pendente => "Pendente",
                StatusPedidoEnum.Aprovado => "Aprovado",
                StatusPedidoEnum.Rejeitado => "Rejeitado",
                _ => "Desconhecido"
            };
        }
    }
}
