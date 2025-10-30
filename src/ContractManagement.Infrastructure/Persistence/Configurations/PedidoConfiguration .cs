using ContractManagement.Domain.Entity.Pedidos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ContractManagement.Infrastructure.Persistence.Configurations
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("pedido");
            builder.HasKey(p => p.Id);
            builder.OwnsOne(p => p.ValorTotal, valortotal =>
            {
                valortotal.Property(p => p.Value).HasColumnName("valor_total").HasColumnType("numeric");
            });
            builder.Property(p => p.DataCriacao).HasColumnName("dt_created").IsRequired();
            builder.Property(p => p.DataAtualizao).HasColumnName("dt_update");
            builder.HasIndex(p => p.Numero).IsUnique();
            builder.Property(p => p.Numero).HasColumnName("numero_pedido").HasMaxLength(20).IsRequired();
            builder.Property(p => p.IdCliente).HasColumnName("id_cliente").IsRequired();
            builder.OwnsOne(p => p.Status, status =>
            {
                status.Property(s => s.Status).HasColumnType("varchar(10)").HasColumnName("status").IsRequired();                
            });
            builder.OwnsMany(p => p.Items, item =>
            {
            item.ToTable("item_pedido");
            item.WithOwner().HasForeignKey("PedidoId");
            item.Property<Guid>("Id");
            item.HasKey("Id");
            item.Property(i => i.IdProduto).HasColumnName("id_produto").IsRequired();
            item.Property(i => i.Produto).HasColumnName("produto").HasMaxLength(200).IsRequired();
            item.Property(i => i.Quantidade).HasColumnName("quantidade").IsRequired();
            item.OwnsOne(i => i.PrecoUnitario, pu =>
            {
                pu.Property(p => p.Value).HasColumnName("preco_unitario").HasColumnType("numeric").IsRequired();
            });
                item.Ignore(i => i.SubTotal);
                //item.Property<decimal>("SubTotal").HasColumnName("sub_total").HasPrecision(18, 2).HasComputedColumnSql("[quantidade] * [preco_unitario]");
            });
            builder.Navigation(p => p.Items).Metadata.SetField("_Items");
            builder.Navigation(p => p.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
