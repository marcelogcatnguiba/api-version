using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiVersion.Api.Configure;

public static class ConfigureSwaggerEndpointDiscovery
{
    public static IServiceCollection AddSwaggerExtension(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>,ConfigureSwaggerOptions>();      

        return services;
    }

    public static void AddSwaggerConfigurationExtension(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            // Geracao de um endpoint do Swagger para cada versao descoberta
            var apiVersionDescriptions = app.Services
                .GetRequiredService<IApiVersionDescriptionProvider>()
                .ApiVersionDescriptions;

            foreach (var group in apiVersionDescriptions.Select(x => x.GroupName))
            {
                options.SwaggerEndpoint($"/swagger/{group}/swagger.json", $"API Microdata APS {group.ToUpperInvariant()}");
            }
        });
    }
}
