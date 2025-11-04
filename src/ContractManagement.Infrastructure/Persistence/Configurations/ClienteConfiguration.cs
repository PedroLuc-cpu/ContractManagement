using ContractManagement.Domain.Entity.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractManagement.Infrastructure.Persistence.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("clientes");
            builder.HasKey(c => c.Id);
            builder.OwnsOne(c => c.FirstName, nome =>
            {
                nome.Property(n => n.Value).HasColumnName("nome").HasMaxLength(50).IsRequired();
            });
            builder.OwnsOne(c => c.LastName, sobrenome =>
            {
                sobrenome.Property(s => s.Value).HasColumnName("sobrenome").HasMaxLength(50).IsRequired();
            });
            builder.OwnsOne(c => c.Email, email =>
            {
                email.Property(e => e.Value).HasColumnName("email").HasMaxLength(100).IsRequired();
                email.HasIndex(e => e.Value).IsUnique();
            });
            builder.OwnsOne(c => c.Endereco, endereco =>
            {
                endereco.Property(e => e.Rua).HasColumnName("rua").HasMaxLength(150).IsRequired();
                endereco.Property(e => e.Numero).HasColumnName("numero").HasMaxLength(4);
                endereco.Property(e => e.Cidade).HasColumnName("cidade").HasMaxLength(150).IsRequired();
                endereco.Property(e => e.Estado).HasColumnName("estado").HasMaxLength(15).IsRequired();
                endereco.Property(e => e.Cep).HasColumnName("cep").HasMaxLength(8).IsRequired();
            });
            builder.Property(c => c.DataCriacao).HasColumnName("dt_created").IsRequired();
            builder.Property(c => c.DataAtualizao).HasColumnName("dt_update");
        }
    }
}
