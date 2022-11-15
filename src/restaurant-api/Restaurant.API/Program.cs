
var builder = WebApplication.CreateBuilder(args)
                            .AddApiConfiguration()
                            .AddSwaggerConfiguration();

var app = builder.Build()
                 .UseApiConfiguration()
                 .UseSwaggerConfiguration();

app.Run();