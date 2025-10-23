using ContractManagement.Domain.Entity.Pedidos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ContractManagement.Application.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("pedido");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ValorTotal).HasColumnName("valor_total").HasColumnType("numeric");
            builder.Property(p => p.DataCriacao).HasColumnName("dt_created").IsRequired();
            builder.Property(p => p.DataAtualizao).HasColumnName("dt_update");

            builder.HasMany(p => p.Items)
                .WithOne()
                .HasForeignKey("pedidoId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.Numero).IsUnique();
        }
    }
}
