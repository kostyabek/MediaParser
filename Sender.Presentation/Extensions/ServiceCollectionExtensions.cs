using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using RestSharp;
using Sender.Domain.Options;
using Sender.Infrastructure.Quartz.Jobs.Posting;
using Telegram.Bot;

namespace Sender.Presentation.Extensions;

/// <summary>
/// Contains extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds and registers Quartz jobs and triggers with hosted service.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Configuration.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddQuartzConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            q.AddJobAndTrigger<PostToTelegramJob>(configuration);
        });

        services.AddQuartzHostedService();

        return services;
    }

    /// <summary>
    /// Adds custom services to DI-container.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Configuration.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<RestClient>();

        services.AddTransient<ITelegramBotClient, TelegramBotClient>(_ =>
            new TelegramBotClient(configuration.GetSection($"{nameof(TelegramOptions)}:{nameof(TelegramOptions.BotToken)}").Value));

        return services;
    }

    /// <summary>
    /// Adds configuration for custom <see cref="IOptions{TOptions}"/>.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Configuration.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddTelegramOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TelegramOptions>(configuration.GetSection(nameof(TelegramOptions)));

        return services;
    }
}