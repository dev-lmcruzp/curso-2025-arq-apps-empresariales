using System.Text.Json.Serialization;
using Asp.Versioning.ApiExplorer;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using TSquad.Ecommerce.Application.UseCases;
using TSquad.Ecommerce.CrossCutting.Logging;
using TSquad.Ecommerce.Persistence;
using TSquad.Ecommerce.Services.WebApi.Modules.Authentication;
using TSquad.Ecommerce.Services.WebApi.Modules.HealthCheck;
using TSquad.Ecommerce.Services.WebApi.Modules.RateLimiter;
using TSquad.Ecommerce.Services.WebApi.Modules.Redis;
using TSquad.Ecommerce.Services.WebApi.Modules.Swagger;
using TSquad.Ecommerce.Services.WebApi.Modules.Versioning;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
const string myPolicy = "AllowSpecificOrigin";
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

builder.Services.AddControllers().AddJsonOptions(options =>
{
    var enumConverter = new JsonStringEnumConverter();
    options.JsonSerializerOptions.Converters.Add(enumConverter);
});

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddLoggingService(builder.Configuration);
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
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
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