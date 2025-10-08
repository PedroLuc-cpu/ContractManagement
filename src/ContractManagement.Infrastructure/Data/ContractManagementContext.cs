using ContractManagement.Domain.Common.Data;
using ContractManagement.Domain.Entity.Pedido;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Infrastructure.Data
{
    public class ContractManagementContext : DbContext, IUnitOfWork
    {
        public DbSet<Pedido> Pedidos { get; set; }
        public ContractManagementContext(DbContextOptions<ContractManagementContext> options) : base(options) 
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
            ChangeTracker.AutoDetectChangesEnabled = false;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
