using ContractManagement.Api.Configuration;
using ContractManagement.Api.OptionSetup;
using ContractManagement.Domain.Interfaces.Services;
using ContractManagement.Infrastructure.Email;
using ContractManagement.Infrastructure.Hubs;
using ContractManagement.Infrastructure.Identity;
using ContractManagement.Infrastructure.Options;
using ContractManagement.Infrastructure.Persistence;
using Marten;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Configuration.SetDefaultConfiguration(builder.Environment);

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

builder.Services.AddDbContext<ContractManagementContext>((serviceProvider, dbContextOptionsBuilder) =>
{
    //var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
    var connectionString = builder.Configuration.GetConnectionString("Default");


    dbContextOptionsBuilder.UseNpgsql(connectionString, postgreSqlAction =>
    {
        postgreSqlAction.EnableRetryOnFailure(3);
        postgreSqlAction.CommandTimeout(30);
    });
    dbContextOptionsBuilder.EnableDetailedErrors(false);
    dbContextOptionsBuilder.EnableSensitiveDataLogging(true);
    dbContextOptionsBuilder.LogTo(Console.WriteLine);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
})
.AddBearerToken(IdentityConstants.BearerScheme)
.AddCookie(IdentityConstants.ApplicationScheme, options =>
{
    options.Cookie.Name = "cm-auth";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

    options.Events = new CookieAuthenticationEvents
    {
        OnRedirectToLogin = ctx =>
        {
            ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        }
    };
});


builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<ContractManagementContext>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddApiEndpoints()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("Default"); ;

builder.Services.AddAuthorization();

builder.Services.AddStackExchangeRedisCache(options =>
    options.Configuration = builder.Configuration.GetConnectionString("Cache")
);

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

builder.Services.AddMediatR(m =>
{
    var MediatRLicence = builder.Configuration.GetConnectionString("MediarRLicence");

    m.LicenseKey = MediatRLicence;
    m.RegisterServicesFromAssembly(ContractManagement.Application.AssemblyReference.Assembly);
});

builder.Services.AddTransient<IEmailSender<ApplicationUser>, SmtpEmailService>();


builder.Services
    .AddControllers()
    .AddApplicationPart(ContractManagement.Presentation.AssemblyReference.Assembly);

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();


if (builder.Environment.EnvironmentName != "Testing")
{
    builder.WebHost
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration();
}

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.ApplyMigration();
//}

app.MapHub<NotificationHub>("/contractmanagementHub");

app.UseSwaggerConfiguration(builder.Environment);

app.UseCors("Total");

if (builder.Environment.IsProduction())
{
    app.UseHttpsRedirection();

}
app.UseAuthentication();

app.UseAuthorization();

app.MapIdentityApi<ApplicationUser>();

app.MapControllers();


app.Run();

public partial class Program { }