using RestaurantReserves.Restaurant.API.Configuration;

namespace RestaurantReserves.Restaurant.API.Extensions
{
    public static class ApplicationConfigurationExtensions
    {
        public static void AddApplicationConfigurations(this WebApplicationBuilder builder, params Type[] scanMarkers)
        {
            var configurations = new List<IConfigurationDefinition>();

            foreach (var scanMarker in scanMarkers)
                configurations.AddRange(scanMarker.Assembly.ExportedTypes
                                                    .Where(e => typeof(IConfigurationDefinition).IsAssignableFrom(e) && e.BaseType is not null)
                                                    .Select(Activator.CreateInstance).Cast<IConfigurationDefinition>().OrderBy(c => c.ConfigurationOrder));

            foreach (var configuration in configurations)
                configuration.AddConfigurations(builder);

            builder.Services.AddSingleton(configurations as IReadOnlyCollection<IConfigurationDefinition>);
        }

        public static void UseApplicationConfigurations(this WebApplication app)
        {
            var configurations = app.Services.GetRequiredService<IReadOnlyCollection<IConfigurationDefinition>>();

            foreach (var configuration in configurations)
                configuration.UseConfigurations(app);
        }
    }
}
