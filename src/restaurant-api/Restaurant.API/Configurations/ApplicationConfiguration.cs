namespace Restaurant.API.Configurations
{
    public static class ApplicationConfiguration
    {
        public static WebApplicationBuilder AddApplicationConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(typeof(CreateRestaurantCommand));

            builder.Services.AddAutoMapper(typeof(RestaurantProfile));

            return builder;
        }
    }
}
