
var builder = WebApplication.CreateBuilder(args)
                            .AddApiConfiguration()
                            .AddCoreConfiguration()
                            .AddInfrastructureConfiguration()
                            .AddSwaggerConfiguration();

var app = builder.Build()
                 .UseApiConfiguration()
                 .UseInfrastructureConfiguration()
                 .UseSwaggerConfiguration();

app.Run();