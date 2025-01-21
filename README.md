# Versionando uma API
Como fazer o versionamento de uma api com .NET

## Package
- Asp.Versioning.Mvc
- Asp.Versioning.Mvc.ApiExplorer

## Configuração
Class Program para contexto
``` csharp
//Registro do Servico
builder.Services.AddSwaggerExtension();

//Add versionamento
builder.Services.ApplyApiVersioningExtension();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //Add config
    app.AddSwaggerConfigurationExtension();
}
```


Principal configuração, extensão de IServiceCollection:
``` csharp
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
            var baseController = opt.Conventions.Controller<BaseController>();
            baseController.HasApiVersion(new(1));

            var controllerTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(BaseController)) && !t.IsAbstract)
                .ToList();

            //Aplica a versao default para todas as endpoints que herdam de BaseHealth...
            foreach(var controller in controllerTypes)
            {
                var convention = opt.Conventions.Controller(controller);
                convention.HasApiVersion(new(1));
                convention.HasApiVersion(new(2)); //Permite omitir atributo nos controller
            }
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
    }
}
```

Documentação para cada versão descoberta:

```csharp
app.UseSwaggerUI(options =>
        {
            // Geracao de um endpoint do Swagger para cada versao descoberta
            var apiVersionDescriptions = app.Services
                .GetRequiredService<IApiVersionDescriptionProvider>()
                .ApiVersionDescriptions;
            
            foreach (var group in apiVersionDescriptions.Select(x => x.GroupName))
            {
                options.SwaggerEndpoint($"/swagger/{group}/swagger.json", $"API Version {group.ToUpperInvariant()}");
            }
        });
```

Mais configuração para Swagger necessaria:
```csharp
public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }
```

## Dicas
Quando possuir uma classe generica tipada por `<T>`, ao tentar aplicar as Conventions, utilizando o reflection, não é possivel instaciar sem informar o tipo pode se fazer a seguinte abordagem:

```csharp
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
public class UserApiVersionAttribute(double version) : ApiVersionAttribute(version)
{
    
}
```

Fazer um Adapter do Attributo da lib, **sobreescrevendo a herança** `(Inherited)` para `true`, por padrão ela é falsa, assim ao aplicar na classe generica, as subclasses receberam o atributo.
