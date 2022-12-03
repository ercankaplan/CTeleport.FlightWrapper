using CTeleport.FlightWrapper.Api.Extentions;
using CTeleport.FlightWrapper.Core.Configuration;
using FluentValidation.AspNetCore;
using Serilog;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Get environment configurations

var configuration = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

// Add services to the container.

AppSettings appSettings = builder.Services.ConfigureServices(configuration, builder.Environment);

//Add validator

builder.Services.AddControllers()
                .AddFluentValidation(options =>
                {
                    // Validate child properties and root collection elements
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;

                    // Automatic registration of validators in assembly
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });

//create the logger and setup your sinks, filters and properties
/*
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
*/

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateBootstrapLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.MapHealthChecks("/healthcheck");

app.ConfigureRequestPipeline(builder.Environment,appSettings);

app.Run();
