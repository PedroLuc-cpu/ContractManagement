using ContractManagement.Domain.Converters;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace ContractManagement.Api.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterServices(services);
            services.AddHttpContextAccessor();
            services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
                options.UseRoutePrefix("v1/api");
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
                options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            });

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            services.AddSignalR();

            string[]? UrlEIpsAutorizados = configuration.GetSection("UrlEIpsAutorizados").Get<string[]>();


            services.AddCors(options =>
            {
                options.AddPolicy("Total", builder =>
                {
                    builder
                    .AllowAnyMethod()
                    .WithOrigins(UrlEIpsAutorizados ?? [])
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.AddEndpointsApiExplorer();

            return services;
        }

        public static WebApplication UseApiConfiguration(this WebApplication app)
        {
            app.UseCors("Total");
            app.UseIdentityConfiguration();
            app.MapControllers();

           
            return app;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddHealthChecks();
        }        
    }
}
