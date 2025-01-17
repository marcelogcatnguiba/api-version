using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiVersion.Api.Configure;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwaggerExtension(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>,ConfigureSwaggerOptions>();      

        return services;
    }
}
