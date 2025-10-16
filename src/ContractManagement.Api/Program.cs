using ContractManagement.Api.Configuration;
using ContractManagement.Api.OptionSetup;
using ContractManagement.Infrastructure.Options;
using ContractManagement.Infrastructure.Persistence;
using Marten;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetDefaultConfiguration(builder.Environment);
builder.Services.ConfigureOptions<DatabaseOptionsSetup>();
builder.Services.AddDbContext<ContractManagementContext>((serviceProvider, dbContextOptionsBuilder) =>
{
    var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

    var connectionString = builder.Configuration.GetConnectionString("Default");
    dbContextOptionsBuilder.UseNpgsql(connectionString, postgreSqlAction =>
    {
        postgreSqlAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
        postgreSqlAction.CommandTimeout(databaseOptions.CommandTimeout);
    });
    dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
    dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
});

builder.Services.AddMarten(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default");
    options.Connection(connectionString);
});


builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration(builder.Environment);

builder.Services.Scan(
    select => select.FromAssemblies(
        ContractManagement.Infrastructure.AssemblyReference.Assembly, 
        ContractManagement.Persistence.AssemblyReference.Assembly)
    .AddClasses(false)
    .AsImplementedInterfaces()
    .WithScopedLifetime());

builder.Services.AddMediatR(m => m.RegisterServicesFromAssembly(ContractManagement.Application.AssemblyReference.Assembly));

builder.Services
    .AddControllers()
    .AddApplicationPart(ContractManagement.Presentation.AssemblyReference.Assembly);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();


if (builder.Environment.EnvironmentName != "Testing")
{
    builder.WebHost
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration();
}

var app = builder.Build();

app.UseSwaggerConfiguration(builder.Environment);

app.UseCors("Total");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }