using ContractManagement.Domain.Common.Exceptions;
using ContractManagement.Domain.Common.Validations;
using ContractManagement.Domain.Enums;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Entity.Solicitacao
{
    public class SolicitacaoInterna: AggregateRoot
    {
        public Guid IdFuncionario { get; private set; }
        public string Titulo { get; private set;  } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public SolicitacaoInternaMetodo Status { get; private set; }
        public Periodo? Periodo { get; private set; }

        private readonly List<SolitacaoHistorico> _historico = [];
        public IReadOnlyCollection<SolitacaoHistorico> Historico => _historico;

        protected SolicitacaoInterna(): base(id: Guid.Empty, dataCriacao: DateTime.UtcNow) { }

        public SolicitacaoInterna(Guid idFuncionario, Periodo? periodo, string titulo, string descricao) : base(id: Guid.NewGuid(), dataCriacao: DateTime.UtcNow)
        {
            IdFuncionario = idFuncionario;
            Periodo = periodo;
            Status = SolicitacaoInternaMetodo.Criado;
            Titulo = titulo;
            Descricao = descricao;

            AdicionarHistorico(SolicitacaoInternaEnumExtensions.GetDescription(Status), "");
        }

        public void Analisar(string? comentario)
        {
            Guard.Againts<DomainException>(Status != SolicitacaoInternaMetodo.Criado, "Só pode analisar uma solicitação criada");
            Status = SolicitacaoInternaMetodo.EmAnalise;
            AdicionarHistorico(SolicitacaoInternaEnumExtensions.GetDescription(Status), comentario);

        }

        public void Aprovar(string? comentario)
        {
            Guard.Againts<DomainException>(Status != SolicitacaoInternaMetodo.EmAnalise, "Só pode aprovar solicitação em análise");
            Status = SolicitacaoInternaMetodo.Aprovado;
            AdicionarHistorico(SolicitacaoInternaEnumExtensions.GetDescription(Status), comentario);

        }
        public void Rejeitar(string? comentario)
        {
            Guard.Againts<DomainException>(Status != SolicitacaoInternaMetodo.EmAnalise, "Só pode rejeitar solicitação em análise");
            Status = SolicitacaoInternaMetodo.Rejeitado;
            AdicionarHistorico(SolicitacaoInternaEnumExtensions.GetDescription(Status), comentario);
        }

        public void Editar(string titulo, string descricao, Periodo? periodo, string? comentario)
        {
            Guard.Againts<DomainException>(Status != SolicitacaoInternaMetodo.Criado, "Só pode editar uma solicitação já criada");
            Titulo = titulo;
            Descricao = descricao;
            Periodo = periodo;

            AdicionarHistorico(SolicitacaoInternaEnumExtensions.GetDescription(SolicitacaoInternaMetodo.Editado), comentario);
        }


        private void AdicionarHistorico(string acao, string? comentario)
        {
            _historico.Add(SolitacaoHistorico.Criar(Id, acao, comentario));
        }

    }
}
