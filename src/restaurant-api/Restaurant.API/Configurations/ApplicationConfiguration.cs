using Restaurant.Application.Services;
using System.Globalization;

namespace Restaurant.API.Configurations
{
    public static class ApplicationConfiguration
    {
        public static WebApplicationBuilder AddApplicationConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IRestaurantService, RestaurantService>();

            builder.Services.AddAutoMapper(typeof(RestaurantProfile));

            builder.Services.AddMediatR(typeof(CreateRestaurantCommand));

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UseCasesValidationBehavior<,>));

            builder.Services.AddValidatorsFromAssemblyContaining<GetRestaurantsQueryValidator>();

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en-US");

            return builder;
        }
    }
}
