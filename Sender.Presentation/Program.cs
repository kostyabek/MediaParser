using Hosted.Common.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

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
            .AddMediaRepository();
    })
    .Build();

await host.RunAsync();