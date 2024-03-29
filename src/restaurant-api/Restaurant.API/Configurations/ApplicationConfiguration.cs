﻿using System.Globalization;

namespace Restaurant.API.Configurations
{
    public static class ApplicationConfiguration
    {
        public static WebApplicationBuilder AddApplicationConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(RestaurantProfile));

            builder.Services.AddMediatR(typeof(CreateRestaurantCommand));

            builder.Services.AddValidatorsFromAssemblyContaining<GetRestaurantsQueryValidator>();

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en-US");

            return builder;
        }
    }
}
