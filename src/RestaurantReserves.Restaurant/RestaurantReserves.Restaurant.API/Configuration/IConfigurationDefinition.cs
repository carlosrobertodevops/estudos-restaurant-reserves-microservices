namespace RestaurantReserves.Restaurant.API.Configuration
{
    public interface IConfigurationDefinition
    {
        int ConfigurationOrder { get; }
        void AddConfigurations(WebApplicationBuilder builder);
        void UseConfigurations(WebApplication app);
    }
}
