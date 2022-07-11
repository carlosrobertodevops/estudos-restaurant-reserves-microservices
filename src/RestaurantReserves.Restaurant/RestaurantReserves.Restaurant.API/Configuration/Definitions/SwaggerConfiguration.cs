using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Runtime.Serialization;

namespace RestaurantReserves.Restaurant.API.Configuration.Definitions
{
    public class SwaggerConfiguration : IConfigurationDefinition
    {
        public int ConfigurationOrder => 1;

        public void AddConfigurations(WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = builder.Configuration["Swagger:Title"],
                    Description = builder.Configuration["Swagger:Description"],
                    TermsOfService = new Uri(builder.Configuration["Swagger:TermsOfService"]),
                    Contact = new OpenApiContact
                    {
                        Name = builder.Configuration["Swagger:Contact:Name"],
                        Url = new Uri(builder.Configuration["Swagger:Contact:Url"])
                    },
                    License = new OpenApiLicense
                    {
                        Name = builder.Configuration["Swagger:License:Name"],
                        Url = new Uri(builder.Configuration["Swagger:License:Url"])
                    }
                });

                options.DescribeAllParametersInCamelCase();

                options.SchemaFilter<EnumSchemaFilter>();
            });
        }

        public void UseConfigurations(WebApplication app)
        {
            app.UseSwagger();

            app.UseSwaggerUI();
        }
    }

    internal class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                model.Enum.Clear();
                foreach (string enumName in Enum.GetNames(context.Type))
                {
                    System.Reflection.MemberInfo memberInfo = context.Type.GetMember(enumName).FirstOrDefault(m => m.DeclaringType == context.Type);

                    EnumMemberAttribute enumMemberAttribute = memberInfo?.GetCustomAttributes(typeof(EnumMemberAttribute), false).OfType<EnumMemberAttribute>().FirstOrDefault();

                    var label = enumMemberAttribute == null || string.IsNullOrWhiteSpace(enumMemberAttribute.Value)
                     ? enumName
                     : enumMemberAttribute.Value;

                    model.Enum.Add(new OpenApiString(label));
                }
            }
        }
    }
}
