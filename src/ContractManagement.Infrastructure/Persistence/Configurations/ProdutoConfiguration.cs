using ContractManagement.Domain.Entity;
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
            builder.Property(p => p.Nome).HasColumnName("nome").HasMaxLength(200).IsRequired();
            builder.Property(p => p.Observacao).HasColumnName("obersavao").HasMaxLength(200).IsRequired();
            builder.Property(p => p.CodigoBarras).HasColumnName("cod_barras").HasMaxLength(14);
            builder.Property(p => p.Codigo).HasColumnName("codigo").HasMaxLength(10);
            builder.Property(p => p.UnidadeMedida).HasColumnName("und_medida").HasMaxLength(10).IsRequired();
            builder.Property(p => p.PrecoVenda).HasPrecision(18,2).HasColumnName("preco_venda").IsRequired();
            builder.Property(p => p.PrecoCusto).HasPrecision(18, 2).HasColumnName("preco_custo");
            builder.Property(p => p.EstoqueAtual).HasColumnName("estoque_atual").HasMaxLength(200).IsRequired();
            builder.Property(p => p.EstoqueMinimo).HasColumnName("estoque_min").HasMaxLength(200).IsRequired();
            builder.Property(p => p.EstoqueMaximo).HasColumnName("estoque_max").HasMaxLength(200).IsRequired();
            builder.Property(p => p.Ativo).HasColumnName("ativo").IsRequired();
        }
    }
}
