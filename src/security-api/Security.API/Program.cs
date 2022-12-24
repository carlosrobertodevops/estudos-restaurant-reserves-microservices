using Security.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfiguration()
       .AddIdentityManagerConfiguration()
       .AddSwaggerConfiguration();

var app = builder.Build()
                 .UseApiConfiguration()
                 .UseSwaggerConfiguration();

app.Run();
