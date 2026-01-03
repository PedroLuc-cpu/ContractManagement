using ContractManagement.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractManagement.Infrastructure.Persistence.Configurations
{
    internal sealed class EstoqueConfiguration : IEntityTypeConfiguration<Estoque>
    {
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            builder.ToTable("EstoquePedido");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.IdProduto).IsRequired();
            builder.HasIndex(e => e.IdProduto).IsUnique();
            builder.Property(e => e.QuantidadeDisponivel).IsRequired();
            builder.Property(e => e.QuantidadeReservada).IsRequired();
        }
    }
}
