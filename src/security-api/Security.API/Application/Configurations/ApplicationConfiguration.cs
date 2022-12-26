using FluentValidation;
using Security.API.Core.Validators;
using Security.API.UseCases.Login;
using System.Globalization;

namespace Security.API.Application.Configurations
{
    public static class ApplicationConfiguration
    {
        public static WebApplicationBuilder AddApplicationConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(typeof(LoginRequest));

            builder.Services.AddTransient(typeof(IValidator<UserViewModel>), b => new UserViewModelValidator());

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en-US");

            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return builder;
        }
    }
}
