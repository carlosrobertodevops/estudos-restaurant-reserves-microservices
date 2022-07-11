namespace RestaurantReserves.Restaurant.API.Configuration.Definitions
{
    public class ApplicationConfiguration : IConfigurationDefinition
    {
        public int ConfigurationOrder => 0;

        public void AddConfigurations(WebApplicationBuilder builder)
        {
            builder.Configuration
                   .SetBasePath(builder.Environment.ContentRootPath)
                   .AddJsonFile("appsettings.json", true, true)
                   .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                   .AddEnvironmentVariables();

            builder.Services.AddControllers();
        }

        public void UseConfigurations(WebApplication app)
        {
            app.UseHttpsRedirection();

            app.MapControllers();
        }
    }
}
