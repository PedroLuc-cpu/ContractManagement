namespace ContractManagement.Domain.Common.Base
{
    /// <summary>
    /// Representa a classe base para entidades, fornecendo funcionalidade comum, como identificação exclusiva, criação
    /// timestamps, e comparação de igualdade.
    /// </summary>
    /// <remarks>Essa classe foi projetada para ser herdada por outras classes de entidade que exigem um
    /// identificador e metadados básicos, como timestamps de data/hora de criação e atualização. Ele também fornece comparação de igualdade com base
    /// no identificador único (<see cref="Id"/>).  O <see cref="Id"/> é inicializada automaticamente com um
    /// new GUID quando uma instância é criada, a menos que um GUID específico seja fornecido por meio do construtor protegido. O <see
    /// cref="DataCriacao"/> property is set to the current date and time at the moment of instantiation.  The class
    /// sobrecarrega (overrides) <see cref="object.Equals(object?)"/> e <see cref="object.GetHashCode"/> para fornecer igualdade
    ///comparação baseada no <see cref="Id"/> propriedade. Além disso, os operadores de igualdade e desigualdade são
    /// implementadas para comparar instâncias de <see cref="EntityBase"/>.</remarks>
    public abstract class EntityBase: IEquatable<EntityBase>
    {
        protected EntityBase(Guid id, DateTime dataCriacao)
        {
            Id = id;
            DataCriacao = dataCriacao;
        }
        public Guid Id { get; protected init; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime? DataAtualizao { get; protected set; }
        public EntityBase()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.UtcNow;

        }
        protected void SetDataAtualizacao()
        {
            DataAtualizao = DateTime.UtcNow;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not EntityBase other) return false;

            if (ReferenceEquals(this, other)) return true;

            return Id.Equals(other.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(EntityBase? other)
        {
            if (other is null)
            {
                return false;
            }
            if (other.GetType() != GetType()) return false;
            return other.Id == Id;
        }

        public static bool operator ==(EntityBase left, EntityBase right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }
        public static bool operator !=(EntityBase left, EntityBase right) { return !(left == right); }
    }
}
