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
            builder.Property(c => c.DataCriacao).HasColumnName("dt_created").IsRequired();
            builder.Property(c => c.DataAtualizao).HasColumnName("dt_update");
            builder.Property(c => c.Nome.Value).HasColumnName("first_name").HasMaxLength(50).IsRequired();
            builder.Property(c => c.LastName.Value).HasColumnName("last_name").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Email.Value).HasColumnName("email").HasMaxLength(100).IsRequired();
            builder.OwnsOne(c => c.Endereco, endereco =>
            {
                endereco.ToTable("enderecos");
                endereco.Property(e => e.Rua).HasColumnName("rua").HasMaxLength(150).IsRequired();
                endereco.Property(e => e.Numero).HasColumnName("numero").HasMaxLength(4);
                endereco.Property(e => e.Cidade).HasColumnName("cidade").HasMaxLength(150).IsRequired();
                endereco.Property(e => e.Estado).HasColumnName("rua").HasMaxLength(15).IsRequired();
                endereco.Property(e => e.CEP).HasColumnName("cep").HasMaxLength(8).IsRequired();
            });

        }
    }
}
