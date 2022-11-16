namespace Restaurant.API.Configurations
{
    public static class CoreConfiguration
    {
        public static WebApplicationBuilder AddCoreConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddValidatorsFromAssemblyContaining<RestaurantValidator>();

            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return builder;
        }
    }
}
