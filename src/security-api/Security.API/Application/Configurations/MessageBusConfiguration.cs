using Security.API.UseCases.CreateUser;

namespace Security.API.Configurations
{
    public static class MessageBusConfiguration
    {
        public static WebApplicationBuilder AddMessageBus(this WebApplicationBuilder builder)
        {
            builder.Services.AddHostedService<CreateUserEventHandler>();

            builder.Services.AddSingleton<IMessageBusManager, RabbitMQManager>();

            return builder;
        }
    }
}
