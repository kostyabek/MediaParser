using System.Reflection;
using Microsoft.OpenApi.Models;
using Parser.Application.HttpClients;
using Parser.Application.Services.Parsing;
using Parser.Infrastructure.HttpClients;
using Parser.Infrastructure.Services.Parsing;
using RestSharp;

namespace Parser.Api.Extensions;

/// <summary>
/// Class with extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds custom services to DI-container.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Configuration.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHtmlHttpClient, HtmlHttpClient>();
        services.AddSingleton<IJoyReactorParsingService, JoyReactorParsingService>();
        services.AddSingleton<RestClient>();

        return services;
    }

    /// <summary>
    /// Adds Swagger configuration.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        return services.AddSwaggerGen(e =>
        {
            e.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Parser API",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Email = "kostyabek@gmail.com",
                    Name = "Kostiantyn Biektin"
                },
                License = new OpenApiLicense
                {
                    Name = "WTFPL",
                    Url = new Uri("https://wtfpl.net")
                },
                TermsOfService = new Uri("https://www.termsfeed.com/blog/sample-terms-of-service-template/#:~:text=A%20Terms%20of%20Service%20agreement,maintaining%20control%20over%20your%20platform.")
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            e.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}