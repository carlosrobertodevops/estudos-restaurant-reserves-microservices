﻿using EasyNetQ;
using Restaurant.Core.Events;
using Restaurant.Infrastructure.MessageBus;
using System.Data.SqlClient;

namespace Restaurant.API.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static WebApplicationBuilder AddInfrastructureConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IDatabaseContext, SqlServerContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            builder.Services.AddTransient(o => new SqlConnection(builder.Configuration.GetConnectionString("SqlServerConnection")));

            builder.Services.AddDbContext<SqlServerContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));
            });

            builder.AddMessageBusConfiguration();

            return builder;
        }

        public static WebApplication UseInfrastructureConfiguration(this WebApplication app)
        {
            using var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();

            var context = serviceScope.ServiceProvider.GetRequiredService<IDatabaseContext>();

            if (context.AnyPendingMigrationsAsync().Result)
            {
                context.MigrateAsync();
            }

            return app;
        }
    }
}
