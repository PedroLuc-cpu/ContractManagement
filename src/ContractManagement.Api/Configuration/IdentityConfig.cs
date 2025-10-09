namespace ContractManagement.Api.Configuration
{
    public static class IdentityConfig
    {
        public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
