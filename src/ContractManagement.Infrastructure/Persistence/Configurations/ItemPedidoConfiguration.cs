using ContractManagement.Domain.Entity.Pedidos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractManagement.Infrastructure.Persistence.Configurations
{
    public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.ToTable(nameof(ItemPedido));
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedNever();
            builder.Property(i => i.IdProduto).IsRequired();
            builder.Property(i => i.NomeProduto).HasMaxLength(200).IsRequired();
            builder.OwnsOne(i => i.PrecoUnitario, precoUnitario =>
            {
                precoUnitario.Property(p => p.Value).HasPrecision(18, 2);
            });
            builder.Property(i => i.Quantidade).IsRequired();
        }
    }
}
