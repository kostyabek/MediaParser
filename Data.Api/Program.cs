using System.Reflection;
using Data.Api.Extensions;
using Data.Application;
using Hosted.Common.Extensions;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var configuration = builder.Configuration;
var cqrsAssembly = Assembly.GetAssembly(typeof(MediatrEntryPoint));

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerConfiguration()
    .AddCustomHostedServices()
    .AddNLogConfiguration(configuration)
    .AddMediatR(cqrsAssembly)
    .AddMediaRepository()
    .AddCustomOptions(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
