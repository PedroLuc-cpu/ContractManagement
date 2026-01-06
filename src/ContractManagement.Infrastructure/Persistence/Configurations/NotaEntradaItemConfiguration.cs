using ContractManagement.Domain.Entity.Estoques;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Infrastructure.Persistence.Configurations
{
    internal sealed class NotaEntradaItemConfiguration : IEntityTypeConfiguration<ItemNotaEntrada>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ItemNotaEntrada> builder)
        {
            builder.ToTable(nameof(ItemNotaEntrada));
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.IdProduto).IsRequired();
            builder.Property(e => e.Quantidade).IsRequired();
            builder.Property(e => e.PrecoUnitario).HasPrecision(18, 2).IsRequired();
        }
    }

}
