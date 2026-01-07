using System.ComponentModel;

namespace ContractManagement.Domain.Enums
{
    public enum StatusPedidoEnum : byte
    {
        [Description("Criado")]
        Criado = 0,
        [Description("Pendente")]
        Pendente = 1,
        [Description("Autorizado")]
        Autorizado = 2,
        [Description("Recusado")]
        Recusado = 3,

    }
    public static class StatusPedidoEnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            return value switch {
                StatusPedidoEnum.Criado => "Criado",
                StatusPedidoEnum.Pendente => "Pendente",
                StatusPedidoEnum.Autorizado => "Autorizado",
                StatusPedidoEnum.Recusado => "Recusado",
                _ => "Desconhecido"
            };
        }
    }
}
