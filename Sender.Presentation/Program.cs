using Hosted.Common.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sender.Presentation.Extensions;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(o =>
    {
        o.AddUserSecrets(typeof(Program).Assembly);
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        services
            .AddCustomOptions(configuration)
            .AddTelegramOptions(configuration)
            .AddMediaRepository()
            .AddCustomServices(configuration)
            .AddQuartzConfiguration(configuration);
    })
    .Build();

await host.RunAsync();