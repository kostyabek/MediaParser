using Microsoft.Extensions.Configuration;
using Quartz;

namespace Sender.Presentation.Extensions;

/// <summary>
/// Class for <see cref="IServiceCollectionQuartzConfigurator"/> extension methods.
/// </summary>
public static class ServiceCollectionQuartzConfiguratorExtensions
{
    /// <summary>
    /// Registers job with trigger.
    /// </summary>
    /// <typeparam name="T">Job type.</typeparam>
    /// <param name="quartz">Service collection Quartz configurator.</param>
    /// <param name="config">Configuration.</param>
    /// <exception cref="Exception"></exception>
    public static void AddJobAndTrigger<T>(this IServiceCollectionQuartzConfigurator quartz, IConfiguration config)
        where T : IJob
    {
        var jobName = typeof(T).Name;

        var configKey = $"Quartz:{jobName}";
        var cronSchedule = config[configKey];

        if (string.IsNullOrEmpty(cronSchedule))
        {
            throw new Exception($"No Quartz.NET Cron schedule found for job in configuration at {configKey}");
        }

        var jobKey = new JobKey(jobName);
        quartz.AddJob<T>(o => o.WithIdentity(jobKey));

        quartz.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity($"{jobName}-trigger")
            .WithCronSchedule(cronSchedule));
    }
}