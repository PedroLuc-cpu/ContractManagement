using ContractManagement.Domain.Entity.Solicitacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractManagement.Infrastructure.Persistence.Configurations
{
    internal sealed class RequestInternalConfiguration : IEntityTypeConfiguration<SolicitacaoInterna>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoInterna> builder)
        {
            builder.ToTable("Solicitacoes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Status).HasConversion<string>().IsRequired();

            builder.OwnsOne(x => x.Periodo, periodo =>
            {
                periodo.Property(p => p.Inicio).HasColumnName("inicio");
                periodo.Property(p => p.Fim).HasColumnName("fim");
            });

            builder.HasMany(x => x.Historico)
               .WithOne()
               .HasForeignKey(x => x.IdSolicitacao)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(x => x.Historico).UsePropertyAccessMode(PropertyAccessMode.Field);

            //builder.HasMany<SolitacaoHistorico>("_historico")
            //    .WithOne()
            //    .HasForeignKey(h => h.IdSolicitacao)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.Navigation("_historico")
            //    .UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
