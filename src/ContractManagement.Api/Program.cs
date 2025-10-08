

using ContractManagement.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetDefaultConfiguration(builder.Environment);
builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration(builder.Environment);

if (builder.Environment.EnvironmentName != "Testing")
{
    builder.WebHost
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration();
}

var app = builder.Build();

app.UseSwaggerConfiguration(builder.Environment);
app.UseApiConfiguration();


app.Run();

public partial class Program { }