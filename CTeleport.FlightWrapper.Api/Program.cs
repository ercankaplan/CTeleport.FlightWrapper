using CTeleport.FlightWrapper.Api.Extentions;
using CTeleport.FlightWrapper.Core.Configuration;
using FluentValidation.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

// Add services to the container.

AppSettings appSettings = builder.Services.ConfigureServices(configuration, builder.Environment);

builder.Services.AddControllers()
                .AddFluentValidation(options =>
                {
                    // Validate child properties and root collection elements
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;

                    // Automatic registration of validators in assembly
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });
var app = builder.Build();

// Configure the HTTP request pipeline.

//app.MapHealthChecks("/healthcheck");

app.ConfigureRequestPipeline(builder.Environment,appSettings);

app.Run();
