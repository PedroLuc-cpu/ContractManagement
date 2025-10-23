using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ContractManagement.Infrastructure.Options
{
    public class DatabaseOptionsSetup(IConfiguration configuration) : IConfigureOptions<DatabaseOptions>
    {
        private const string ConfigurationSectionName = "DataBaseOptions";
        private readonly IConfiguration _configuration = configuration;

        public void Configure(DatabaseOptions options)
        {
            var connetionString = _configuration.GetConnectionString("Default")!;

            options.ConnectionString = connetionString;

            _configuration.GetSection(ConfigurationSectionName).Bind(options);
            
        }
    }
}
