using ContractManagement.Domain.Entity.Estoques;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractManagement.Infrastructure.Persistence.Configurations
{
    internal sealed class NotaEntradaConfiguration : IEntityTypeConfiguration<NotaEntrada>
    {
        public void Configure(EntityTypeBuilder<NotaEntrada> builder)
        {
            builder.ToTable(nameof(NotaEntrada));
            builder.HasKey(ne => ne.Id);
            builder.Property(ne => ne.NumeroDocumento)
                .IsRequired();
            builder.Property(ne => ne.DataEntrada)
                .IsRequired();
            builder.HasMany(n => n.Itens)
                        .WithOne()
                        .HasForeignKey("nota_entrada_id")
                        .OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(ne => ne.Itens)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
