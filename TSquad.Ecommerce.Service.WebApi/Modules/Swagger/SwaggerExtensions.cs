using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TSquad.Ecommerce.Service.WebApi.Modules.Swagger;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(c =>
        {
            // Con el versionado se migro a ConfigureSwaggerOptions
            // c.SwaggerDoc("v1", new OpenApiInfo()
            // {
            //     Version = "v1",
            //     Title = "T-Squad API",
            //     Description = "A simple example ASP.NET Core Web API",
            //     TermsOfService = new Uri("https://example.com/terms"),
            //     Contact = new OpenApiContact
            //     {
            //         Name = "Yo",
            //         Email = "yo@yopmail.com",
            //         Url = new Uri("https://yopmail.com"),
            //     },
            //     License = new OpenApiLicense
            //     {
            //         Name = "Use under LICX",
            //         Url = new Uri("https://yopmail.com"),
            //     }
            // });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter JWT Bearer Token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme =  "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme,
                }
            };
            
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {securityScheme, new List<string> { }}
            });
            
            
            c.EnableAnnotations();
        });
        return services;
    }
}