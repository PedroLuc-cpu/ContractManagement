using ContractManagement.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractManagement.Infrastructure.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.DescricaoStatus).HasColumnType("varchar");
            builder.Property(u => u.Entrada).HasColumnType("timestamp without time zone").HasDefaultValue(new DateTime(2024,1,1));
            builder.Property(u => u.Saida).HasColumnType("timestamp without time zone").HasDefaultValue(new DateTime(2024, 1, 1));
        }
    }
}
