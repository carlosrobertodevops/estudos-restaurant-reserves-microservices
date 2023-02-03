using Security.API.Core.ExternalServices;

namespace Security.API.Configurations
{
    public static class IdentityManagerConfiguration
    {
        public static WebApplicationBuilder AddIdentityManagerConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient(ClientExtensions.KeycloakClient, client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["Keycloak:BaseAddress"]);
            });

            builder.Services.AddTransient<IIdentityManager, KeycloakManager>();

            return builder;
        }
    }
}
