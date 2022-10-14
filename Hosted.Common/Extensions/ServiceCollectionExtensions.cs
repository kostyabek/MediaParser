using System.Net.Mail;
using System.Text;
using Application.Common.Repositories;
using Domain.Common.Options;
using Hosted.Common.Services;
using Infrastructure.Common.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Layouts;
using NLog.Targets;

namespace Hosted.Common.Extensions;

/// <summary>
/// Contains extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds NLog configuration.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Configuration.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddNLogConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var nLogMailSection = configuration.GetSection("NLog:Mail");
        var nLogConsoleSection = configuration.GetSection("NLog:Console");

        var nLogConfig = new LoggingConfiguration();
        var logConsole = new ColoredConsoleTarget("logConsole")
        {
            Layout = nLogConsoleSection["Layout"]
        };
        var logMail = new MailTarget("logMail")
        {
            From = nLogMailSection["From"],
            To = nLogMailSection["To"],
            Priority = nLogMailSection["Priority"],
            AddNewLines = true,
            Body = new JsonLayout
            {
                Attributes =
                {
                    new JsonAttribute("date", Layout.FromString("${date}")),
                    new JsonAttribute("level", Layout.FromString("${level:uppercase=true}")),
                    new JsonAttribute("message", Layout.FromString("${message}")),
                    new JsonAttribute("exception", Layout.FromString("${exception:format=ToString}")),
                    new JsonAttribute("exceptionData", Layout.FromString("${exceptionData}")),
                },
                MaxRecursionLimit = 3,
                IncludeEventProperties = true,

            },
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Encoding = Encoding.UTF8,
            SmtpAuthentication = SmtpAuthenticationMode.Basic,
            Subject = nLogMailSection["Subject"],
            SmtpPort = Convert.ToInt32(nLogMailSection["SmtpPort"]),
            SmtpServer = nLogMailSection["SmtpServer"],
            SmtpUserName = nLogMailSection["SmtpUsername"],
            SmtpPassword = nLogMailSection["SmtpPassword"]
        };

        nLogConfig.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logConsole);
        nLogConfig.AddRule(NLog.LogLevel.Error, NLog.LogLevel.Fatal, logMail);

        LogManager.Configuration = nLogConfig;

        services.AddLogging(e =>
        {
            e.ClearProviders();
            e.AddNLog(configuration);
        });

        return services;
    }

    /// <summary>
    /// Adds custom hosted services.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddCustomHostedServices(this IServiceCollection services)
    {
        return services.AddHostedService<LifetimeEventsHostedService>();
    }

    /// <summary>
    /// Adds configuration for custom <see cref="IOptions{TOptions}"/>.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Configuration.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(nameof(DatabaseOptions)));

        return services;
    }

    /// <summary>
    /// Registers <see cref="IMediaRepository"/> with <see cref="MediaRepository"/> implementation in DI-container.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddMediaRepository(this IServiceCollection services)
    {
        services.AddScoped<IMediaRepository, MediaRepository>();

        return services;
    }
}