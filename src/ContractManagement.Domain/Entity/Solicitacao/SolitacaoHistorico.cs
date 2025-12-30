using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagement.Domain.Entity.Solicitacao
{
    public class SolitacaoHistorico
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; private set; }
        public Guid IdSolicitacao { get; private set; }
        public string Acao { get; private set;  } = string.Empty;
        public string? Comentario { get; private set;  } = string.Empty;
        public DateTimeOffset OcorridoEm { get; private set; }

        protected SolitacaoHistorico() { }

        public static SolitacaoHistorico Criar (Guid idSolicitacao, string acao, string? comentario )
        {
            return new SolitacaoHistorico{
                Id = Guid.NewGuid(),
                IdSolicitacao = idSolicitacao,
                Acao = acao,
                Comentario = comentario,
                OcorridoEm = DateTimeOffset.UtcNow,
            };
        }

    }
}
