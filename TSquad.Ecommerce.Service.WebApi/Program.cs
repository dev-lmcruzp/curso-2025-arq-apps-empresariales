using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using TSquad.Ecommerce.Application.Main;
using TSquad.Ecommerce.CrossCutting.Logging;
using TSquad.Ecommerce.Domain.Core;
using TSquad.Ecommerce.Infrastructure.Repository;
using TSquad.Ecommerce.Service.WebApi.Modules.Authentication;
using TSquad.Ecommerce.Service.WebApi.Modules.HealthCheck;
using TSquad.Ecommerce.Service.WebApi.Modules.RateLimiter;
using TSquad.Ecommerce.Service.WebApi.Modules.Redis;
using TSquad.Ecommerce.Service.WebApi.Modules.Swagger;
using TSquad.Ecommerce.Service.WebApi.Modules.Validator;
using TSquad.Ecommerce.Service.WebApi.Modules.Versioning;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var myPolicy = "AllowSpecificOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(myPolicy,
        b =>
        {
            b.WithOrigins(configuration["Config:OriginCors"]!) // Replace with your allowed origin(s)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddDomainService();
builder.Services.AddRepository();
builder.Services.AddApplicationService();
builder.Services.AddLoggingService(builder.Configuration);
builder.Host.UseSerilog();


builder.Services.AddAuth(builder.Configuration);
builder.Services.AddValidator();
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
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();
app.UseEndpoints(_ => { });

app.UseCors(myPolicy);

app.MapControllers();
// app.MapHealthChecks("/health");
app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    /*ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new { e.Key, status = e.Value.Status.ToString() })
        };
        await context.Response.WriteAsJsonAsync(result);
    }*/
});
app.MapHealthChecksUI();

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