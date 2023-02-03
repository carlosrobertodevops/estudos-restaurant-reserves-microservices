
var builder = WebApplication.CreateBuilder(args)
                            .AddApiConfiguration()
                            .AddCoreConfiguration()
                            .AddInfrastructureConfiguration()
                            .AddApplicationConfiguration();

var app = builder.Build()
                 .UseApiConfiguration()
                 .UseInfrastructureConfiguration();

app.Run();