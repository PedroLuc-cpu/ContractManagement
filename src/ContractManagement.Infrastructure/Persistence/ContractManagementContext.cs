using ContractManagement.Domain.Entity.Pedido;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Infrastructure.Persistence
{
    public class ContractManagementContext : DbContext
    {
        public DbSet<PedidoEntity> Pedidos { get; set; }
        public DbSet<ItemPedidoEntity> ItemPedido { get; set; }
        public ContractManagementContext(DbContextOptions<ContractManagementContext> options) : base(options) 
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
            ChangeTracker.AutoDetectChangesEnabled = false;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
