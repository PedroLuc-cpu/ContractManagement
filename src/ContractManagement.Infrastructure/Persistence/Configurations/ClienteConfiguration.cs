using ContractManagement.Domain.Entity.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractManagement.Infrastructure.Persistence.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable(nameof(Cliente));
            builder.HasKey(c => c.Id);
            builder.OwnsOne(c => c.FirstName, nome =>
            {
                nome.Property(n => n.Value).HasMaxLength(50).IsRequired();
            });
            builder.OwnsOne(c => c.LastName, sobrenome =>
            {
                sobrenome.Property(s => s.Value).HasMaxLength(50).IsRequired();
            });
            builder.OwnsOne(c => c.Email, email =>
            {
                email.Property(e => e.Value).HasMaxLength(100).IsRequired();
                email.HasIndex(e => e.Value).IsUnique();
            });
            builder.OwnsOne(c => c.Endereco, endereco =>
            {
                endereco.ToTable("Endereco_Cliente");
                endereco.Property(e => e.Rua).HasMaxLength(150).IsRequired();
                endereco.Property(e => e.Numero).HasMaxLength(4);
                endereco.Property(e => e.Cidade).HasMaxLength(150).IsRequired();
                endereco.Property(e => e.Estado).HasMaxLength(15).IsRequired();
                endereco.Property(e => e.Cep).HasMaxLength(8).IsRequired();
            });
            builder.Property(c => c.DataCriacao).IsRequired();
            builder.Property(c => c.DataAtualizao);
        }
    }
}
