using ContractManagement.Api.Configuration;
using ContractManagement.Infrastructure.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetDefaultConfiguration(builder.Environment);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration(builder.Environment);

builder.Services.Scan(
    select => select.FromAssemblies(
        ContractManagement.Infrastructure.AssemblyReference.Assembly, 
        ContractManagement.Persistence.AssemblyReference.Assembly)
    .AddClasses(false)
    .AsImplementedInterfaces()
    .WithScopedLifetime());

builder.Services
    .AddControllers()
    .AddApplicationPart(ContractManagement.Presentation.AssemblyReference.Assembly);

if (builder.Environment.EnvironmentName != "Testing")
{
    builder.WebHost
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration();
}

var app = builder.Build();

app.UseSwaggerConfiguration(builder.Environment);
//app.UseApiConfiguration();

app.UseCors("Total");
app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();

app.Run();

public partial class Program { }