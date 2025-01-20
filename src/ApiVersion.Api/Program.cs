using ApiVersion.Api.Configure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerExtension();
builder.Services.ApplyApiVersioningExtension();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.AddSwaggerConfigurationExtension();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();