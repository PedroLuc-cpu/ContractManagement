namespace ContractManagement.Api.Configuration
{
    public static class ConfigurationBuilder
    {
        public static IConfiguration SetDefaultConfiguration(this ConfigurationManager configuration, IWebHostEnvironment hostEnvironment)
        {
            var builder = configuration
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            if (hostEnvironment.EnvironmentName != "Production")
            {
                builder.AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json");
            }
            return builder.Build();
        }
    }
}
