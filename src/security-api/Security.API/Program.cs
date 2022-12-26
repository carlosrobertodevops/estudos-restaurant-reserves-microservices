using Security.API.Application.Configurations;
using Security.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfiguration()
       .AddApplicationConfiguration()
       .AddIdentityManagerConfiguration()
       .AddMessageBus()
       .AddSwaggerConfiguration();

var app = builder.Build()
                 .UseApiConfiguration()
                 .UseSwaggerConfiguration();

app.Run();
