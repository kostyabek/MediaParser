using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Data.Api.Extensions
{
    /// <summary>
    /// Contains extensions methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
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
                    Title = "Data API",
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
}
