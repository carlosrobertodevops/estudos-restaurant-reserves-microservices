using EasyNetQ;
using EasyNetQ.DI;
using Restaurant.Core.Events;
using Restaurant.Infrastructure.MessageBus;
using System.Collections.Concurrent;

namespace Restaurant.API.Configurations
{
    public static class MessageBusConfiguration
    {
        public static WebApplicationBuilder AddMessageBusConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IMessageBusManager, RabbitMQManager>();

            return builder;
        }
        
    }
    
}
