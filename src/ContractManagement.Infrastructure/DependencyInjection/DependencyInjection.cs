using ContractManagement.Application.Contracts.Repository.IPedido;
using ContractManagement.Application.Contracts.Services;
using ContractManagement.Application.Interfaces;
using ContractManagement.Application.Services;
using ContractManagement.Infrastructure.Persistence;
using ContractManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContractManagement.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        private static string ConvertDatabaseUrlToNpgsql(string databaseUrl)
        {
            var uri = new Uri(databaseUrl);
            var userInfo = uri.UserInfo.Split(':');
            return $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};Ssl Mode=Require;Trust Server Certificate=true";
        }

        public static IServiceCollection AddInfrastructure(this  IServiceCollection services, IConfiguration configuration)
        {
            var dbUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var connectionString = string.IsNullOrEmpty(dbUrl)
                ? configuration.GetConnectionString("Default")
                : ConvertDatabaseUrlToNpgsql(dbUrl);

            services.AddDbContext<ContractManagementContext>(options =>
            {
                options.UseNpgsql(connectionString,
                                assembly => assembly.MigrationsAssembly(typeof(ContractManagementContext).Assembly.FullName))
                                .EnableSensitiveDataLogging()
                                .EnableDetailedErrors();
                options.UseNpgsql(connectionString, o => o.EnableRetryOnFailure());
            });


            services.AddScoped<ContractManagementContext>();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddScoped<IPedidoRepository, PedidoRepositories>();
            services.AddScoped<IPedidoService, PedidoService>();

            return services;

        }
        
          
    }
}
