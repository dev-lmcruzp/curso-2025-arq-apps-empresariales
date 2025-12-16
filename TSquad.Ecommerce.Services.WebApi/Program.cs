using Asp.Versioning.ApiExplorer;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using TSquad.Ecommerce.Application.Interface.Presentation;
using TSquad.Ecommerce.Application.UseCases;
using TSquad.Ecommerce.CrossCutting.Logging;
using TSquad.Ecommerce.Infrastructure;
using TSquad.Ecommerce.Persistence;
using TSquad.Ecommerce.Services.WebApi.Modules.Authentication;
using TSquad.Ecommerce.Services.WebApi.Modules.Feature;
using TSquad.Ecommerce.Services.WebApi.Modules.HealthCheck;
using TSquad.Ecommerce.Services.WebApi.Modules.Middlewares;
using TSquad.Ecommerce.Services.WebApi.Modules.RateLimiter;
using TSquad.Ecommerce.Services.WebApi.Modules.Redis;
using TSquad.Ecommerce.Services.WebApi.Modules.Swagger;
using TSquad.Ecommerce.Services.WebApi.Modules.Versioning;
using TSquad.Ecommerce.Services.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddFeature(builder.Configuration);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddLoggingService(builder.Configuration);
builder.Services.AddTransient<GlobalExceptionHandler>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Host.UseSerilog();


builder.Services.AddAuth(builder.Configuration);
builder.Services.AddHealthCheck(builder.Configuration);
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddRateLimiter(builder.Configuration);
builder.Services.AddVersioning();
builder.Services.AddSwagger();

var app = builder.Build();


var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        //c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
        c.RoutePrefix = "swagger";
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
        c.ShowExtensions();
    });
    // app.MapOpenApi();

    app.UseReDoc(opts =>
    {
        opts.DocumentTitle = "TSquad technology services API market";
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            opts.SpecUrl = $"/swagger/{description.GroupName}/swagger.json";
        }
    });

}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();
app.UseRequestTimeouts();
app.UseEndpoints(_ => { });

app.UseCors(PresentationConstant.MyPolicyCors);

app.MapControllers();
// app.MapHealthChecks("/health");
app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI();

app.AddMiddleware();

try
{
    Log.Information("Starting TSquad.Ecommerce API");
    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "TSquad.Ecommerce API terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

// para pruebas unitarias
/// <summary>
/// 
/// </summary>
public partial class Program
{
}