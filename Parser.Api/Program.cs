using Parser.Api.Extensions;
using Parser.Application.CQRS;
using System.Reflection;
using Hosted.Common.Extensions;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers();

var configuration = builder.Configuration;

var cqrsAssembly = Assembly.GetAssembly(typeof(MediatrEntryPoint));

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerConfiguration()
    .AddCustomServices(builder.Configuration)
    .AddMediaRepository()
    .AddMediatR(cqrsAssembly)
    .AddNLogConfiguration(builder.Configuration)
    .AddCustomHostedServices()
    .AddCustomOptions(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
