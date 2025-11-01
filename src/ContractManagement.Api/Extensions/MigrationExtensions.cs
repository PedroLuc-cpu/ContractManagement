using ContractManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Api.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            
            using ContractManagementContext dbContext = scope.ServiceProvider.GetService<ContractManagementContext>();

            dbContext.Database.Migrate();
        }
    }
}
