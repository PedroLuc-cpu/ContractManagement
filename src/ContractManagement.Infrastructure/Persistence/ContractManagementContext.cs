using ContractManagement.Domain.Entity.Catalogo;
using ContractManagement.Domain.Entity.Clientes;
using ContractManagement.Domain.Entity.Pedidos;
using ContractManagement.Domain.Entity.Solicitacao;
using ContractManagement.Domain.ValueObjects;
using ContractManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Infrastructure.Persistence
{
    public class ContractManagementContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Promocao> Promocao { get; set; }
        public DbSet<SolicitacaoInterna> SolicitacaoInternas { get; set; }

        public ContractManagementContext(DbContextOptions<ContractManagementContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            ChangeTracker.AutoDetectChangesEnabled = true;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        }
    }
}
