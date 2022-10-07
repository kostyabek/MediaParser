using Microsoft.Extensions.Hosting;
using NLog;

namespace Hosted.Common.Services;

/// <summary>
/// Responsible for execution of lifetime-related application events.
/// </summary>
public class LifetimeEventsHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Instantiates <see cref="LifetimeEventsHostedService"/>.
    /// </summary>
    /// <param name="appLifetime">Host application lifetime.</param>
    public LifetimeEventsHostedService(IHostApplicationLifetime appLifetime)
    {
        _appLifetime = appLifetime;
    }

    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifetime.ApplicationStopping.Register(OnStopping);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void OnStopping()
    {
        _logger.Info("Application is stopping.");
        _logger.Info("Disposing loggers.");
        LogManager.Shutdown();
    }
}