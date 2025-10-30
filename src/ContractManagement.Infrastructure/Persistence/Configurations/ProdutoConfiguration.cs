using ContractManagement.Domain.Entity.Catalogo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractManagement.Infrastructure.Persistence.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("produtos");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome)
                .HasColumnName("nome")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Observacao)
                .HasColumnName("obersavao")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.CodigoBarras)
                .HasColumnName("cod_barras")
                .HasMaxLength(14);

            builder.Property(p => p.Codigo)
                .HasColumnName("codigo")
                .HasMaxLength(10);

            builder.Property(p => p.UnidadeMedida)
                .HasColumnName("und_medida")
                .HasMaxLength(10)
                .IsRequired();

            builder.OwnsOne(p => p.PrecoCusto, p_custo =>
            {
                p_custo.Property(p => p.Value)
                    .HasPrecision(18, 2)
                    .HasColumnName("preco_custo");
            });
            builder.OwnsOne(p => p.PrecoVenda, p_venda =>
            {
                p_venda.Property(p => p.Value)
                .HasPrecision(18, 2)
                .HasColumnName("preco_venda").IsRequired();
            });
            
            builder.OwnsOne(p => p.Disponibilidade, disponibilidade =>
            {
                disponibilidade.ToTable("disponibilidade_produto");
                disponibilidade.Property(d => d.Inicio).HasColumnName("inicio");
                disponibilidade.Property(d => d.Fim).HasColumnName("fim");
            });

            builder.OwnsOne(p => p.Promocao, p_promocao => 
            {
                p_promocao.ToTable("promocao_produto");
                p_promocao.Property(pp => pp.DescontoPercentual).HasPrecision(18, 2).HasColumnName("desconto_percentual");
                p_promocao.OwnsOne(periodo_promo => periodo_promo.Periodo, p_promo =>
                {
                    p_promo.Property(pp => pp.Inicio).HasColumnName("inicio");
                    p_promo.Property(pp => pp.Fim).HasColumnName("fim");
                });
            });

            builder.Property(p => p.EstoqueAtual)
                .HasColumnName("estoque_atual")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.EstoqueMinimo)
                .HasColumnName("estoque_min")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.EstoqueMaximo)
                .HasColumnName("estoque_max")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Ativo)
                .HasColumnName("ativo")
                .IsRequired();
        }
    }
}
