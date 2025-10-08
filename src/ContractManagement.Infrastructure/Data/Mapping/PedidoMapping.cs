
using ContractManagement.Domain.Entity.Pedido;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractManagement.Infrastructure.Data.Mapping
{
    public class PedidoMapping : IEntityTypeConfiguration<PedidoEntity>
    {
        public void Configure(EntityTypeBuilder<PedidoEntity> builder)
        {
            builder.ToTable("pedido");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ValorTotal).HasColumnName("valor_total").HasColumnType("numeric");
            builder.Property(p => p.DataCriacao).HasColumnName("dt_created").IsRequired();
            builder.Property(p => p.DataAtualizao).HasColumnName("dt_update");
        }
    }
}
