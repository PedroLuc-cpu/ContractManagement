using ContractManagement.Domain.Entity.Pedidos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ContractManagement.Infrastructure.Persistence.Configurations
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable(nameof(Pedido));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.OwnsOne(p => p.ValorTotal, valortotal =>
            {
                valortotal.Property(p => p.Value).HasColumnName("ValorTotal").HasPrecision(18,2);
            });
            builder.Property(p => p.DataCriacao).IsRequired();
            builder.Property(p => p.DataAtualizao);

            builder.OwnsOne(p => p.Numero, numero =>
            {
                numero.Property(n => n.Value).HasColumnName("NumeroPedido").HasMaxLength(50).IsRequired();
                numero.HasIndex(n => n.Value).IsUnique();
            });

            builder.Property(p => p.Status).HasColumnName("StatusPedido").HasConversion<string>().IsRequired();

            builder.Property(p => p.IdCliente).HasColumnName("IdCliente").IsRequired();
            
            builder.HasMany(item => item.Items).WithOne().HasForeignKey("PedidoId").OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(p => p.Items).Metadata.SetField("_Items");

            builder.Navigation(p => p.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
