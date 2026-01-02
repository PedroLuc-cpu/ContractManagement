using ContractManagement.Domain.Entity.Catalogo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractManagement.Infrastructure.Persistence.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable(nameof(Produto));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(p => p.Imagem);
            builder.Property(p => p.Observacao)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.CodigoBarras)
                .HasMaxLength(14);

            builder.Property(p => p.Codigo)
                .HasMaxLength(10);

            builder.Property(p => p.UnidadeMedida)
                .HasMaxLength(10)
                .IsRequired();

            builder.OwnsOne(p => p.PrecoCusto, p_custo =>
            {
                p_custo.Property(p => p.Value)
                    .HasPrecision(18, 2)
                    .HasColumnName("PrecoCusto");
            });
            builder.OwnsOne(p => p.PrecoVenda, p_venda =>
            {
                p_venda.Property(p => p.Value)
                .HasPrecision(18, 2)
                .HasColumnName("PrecoVenda").IsRequired();
            });
            
            builder.OwnsOne(p => p.Disponibilidade, disponibilidade =>
            {
                disponibilidade.ToTable("DisponibilidadeProduto");
                disponibilidade.Property(d => d.Inicio);
                disponibilidade.Property(d => d.Fim);
            });

            builder.OwnsOne(p => p.Promocao, p_promocao => 
            {
                p_promocao.ToTable("PromocaoProduto");
                p_promocao.Property(pp => pp.DescontoPercentual).HasPrecision(18, 2);
                p_promocao.OwnsOne(periodo_promo => periodo_promo.Periodo, p_promo =>
                {
                    p_promo.Property(pp => pp.Inicio);
                    p_promo.Property(pp => pp.Fim);
                });
            });

            builder.Property(p => p.EstoqueAtual)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.EstoqueMinimo)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.EstoqueMaximo)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Ativo)
                .IsRequired();
        }
    }
}
