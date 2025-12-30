using System.ComponentModel;

namespace ContractManagement.Domain.Enums
{
    public enum SolicitacaoInternaMetodo : byte
    {
        [Description("Criado")]
        Criado = 1,
        [Description("Aprovado")]
        Aprovado = 2,
        [Description("Rejeitado")]
        Rejeitado = 3,
        [Description("Em Analise")]
        EmAnalise = 4,
        [Description("Editado")]
        Editado = 5,
    }
    public static class SolicitacaoInternaEnumExtensions
    {
        public static string GetDescription(this Enum value )
        {
            return value switch
            {
                SolicitacaoInternaMetodo.Aprovado => "Solicitação aprovada",
                SolicitacaoInternaMetodo.Rejeitado => "Solicitação rejeitada",
                SolicitacaoInternaMetodo.Criado => "Solicitação criada",
                SolicitacaoInternaMetodo.EmAnalise => "Solicitação em analise",
                SolicitacaoInternaMetodo.Editado => "Solicitação editada",
                _ => value.ToString(),
            };
        }
    }
}
