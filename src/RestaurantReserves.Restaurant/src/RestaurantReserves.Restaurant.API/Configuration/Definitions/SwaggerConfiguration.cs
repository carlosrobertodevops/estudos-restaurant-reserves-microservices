namespace RestaurantReserves.Restaurant.API.Configuration.Definitions
{
    public class SwaggerConfiguration : IConfigurationDefinition
    {
        public int ConfigurationOrder => (int)ConfigurationsOrder.SWAGGER;

        public void AddConfigurations(WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                //foreach (var description in _provider.ApiVersionDescriptions)
                //{
                //    options.SwaggerDoc
                //        (
                //            description.GroupName,
                //            CreateInfoForApiVersion(builder, description)
                //        );

                //    options.OrderActionsBy(selector => $"{selector.HttpMethod}_{selector.GroupName}_{selector.ActionDescriptor.RouteValues["action"]}");
                //}

                options.SwaggerDoc
                        (
                            "v1",
                            CreateInfoForApiVersion(builder, null)
                        );

                options.DescribeAllParametersInCamelCase();

                options.OperationFilter<SwaggerDefaultValues>();

                options.SchemaFilter<EnumSchemaFilter>();
            });

            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            builder.Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        public void UseConfigurations(WebApplication app)
        {
            app.UseSwagger();

            app.UseSwaggerUI();
        }

        private static OpenApiInfo CreateInfoForApiVersion(WebApplicationBuilder builder, ApiVersionDescription description = null)
        {
            var info = new OpenApiInfo
            {
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
            };

            if (description is not null && description.IsDeprecated)
                info.Description += " This Api version has been deprecated";

            return info;
        }
    }

    internal class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated |= apiDescription.IsDeprecated();

            if (operation.Parameters == null) return;

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                if (parameter.Description == null)
                    parameter.Description = description.ModelMetadata?.Description;

                if (parameter.Schema.Default == null && description.DefaultValue != null)
                    parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());

                parameter.Required |= description.IsRequired;
            }
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
