using ContractManagement.Domain.Entity.Pedidos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractManagement.Application.Mappings
{
    public class ItemPedido : IEntityTypeConfiguration<Domain.Entity.Pedidos.ItemPedido>
    {
        public void Configure(EntityTypeBuilder<Domain.Entity.Pedidos.ItemPedido> builder)
        {
            builder.ToTable(nameof(ItemPedido).ToLower());
            builder.Property(i => i.Id).ValueGeneratedNever();
            builder.Property(i => i.Id).ValueGeneratedNever();
            builder.Property(i => i.IdProduto).HasColumnName("id_pedidos").IsRequired();
            builder.Property(i => i.Produto).IsRequired().HasMaxLength(200);
            builder.Property(i => i.Quantidade)
               .IsRequired();
            builder.Property(i => i.PrecoUnitario)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // para não persistir no banco de dados
            builder.Ignore(i => i.SubTotal);
        }
    }
}
