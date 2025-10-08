using ContractManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace ContractManagement.Api.Configuration
{
    public static class IdentityConfig
    {
        private static string ConvertDatabaseUrlToNpgsql(string databaseUrl)
        {
            var uri = new Uri(databaseUrl);
            var userInfo = uri.UserInfo.Split(':');
            return $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};Ssl Mode=Require;Trust Server Certificate=true";
        }

        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
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

            return services;

        }

        public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
