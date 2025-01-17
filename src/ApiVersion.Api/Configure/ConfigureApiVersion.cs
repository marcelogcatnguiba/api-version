using System.Reflection;
using ApiVersion.Api.Controllers;
using Asp.Versioning;

namespace ApiVersion.Api.Configure;

public static class ConfigureApiVersion
{
    public static void ApplyApiVersioningExtension(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            
            //Especifica versionamento por Url
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddMvc(opt => 
        {
            //Default Version
            var baseController = opt.Conventions.Controller<BaseHealthCheckController>();

            //TODO: Enviar pra um arquivo de configuracao
            baseController.HasApiVersion(new(1));

            var controllerTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(BaseHealthCheckController)) && !t.IsAbstract)
                .ToList();

            //Aplica a versao default para todas as endpoints que herdam de BaseHealth...
            foreach(var controller in controllerTypes)
            {
                var convention = opt.Conventions.Controller(controller);
                convention.HasApiVersion(new(1));
            }
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
    }
}
