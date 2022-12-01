using CTeleport.FlightWrapper.Api.Infrastructure.Filters;
using CTeleport.FlightWrapper.Core.Configuration;
using CTeleport.FlightWrapper.Core.HttpClient;
using CTeleport.FlightWrapper.Core.Infrastructure;
using CTeleport.FlightWrapper.Core.Interfaces;
using CTeleport.FlightWrapper.Service.Airports;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;
using System.Reflection;

namespace CTeleport.FlightWrapper.Api.Extentions
{
    /// <summary>
    /// Represents extensions of IServiceCollection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static AppSettings AppSettings { get; set; }
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// Add services to the application and configure service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        /// <param name="environment">Hosting environment</param>
        /// <returns>Configured engine and app settings</returns>
        public static AppSettings ConfigureServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
           

            //add configuration parameters
            var appSettings = new AppSettings();
            configuration.Bind(appSettings);
            services.AddSingleton(appSettings);

            Singleton<AppSettings>.Instance = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            Singleton<IWebHostEnvironment>.Instance = environment;

            AppSettings = appSettings;
            Configuration = configuration;

            services.AddControllers(options => options.Filters.Add(typeof(ModelStateFilter)));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(o => AddSwaggerDocumentation(o));

            services.AddSwaggerGen();

            //add options feature
            services.AddOptions();

            //add caching
            services.AddMemoryCache();

            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            //add logging
            //services.AddLogger();

            //register custom services
            services.AddHttpClients(configuration);
            services.AddServices();

            //add automapper
            //services.AddAutoMapper();

            //add auth
            //services.AddAuthentication();

            ////add versioning
            //services.AddApiVersioning();

            //add routing
            services.AddRouting(options => options.LowercaseUrls = true);

            return appSettings;
        }

        /// <summary>
        /// Create, bind and register as service the specified configuration parameters 
        /// </summary>
        /// <typeparam name="TConfig">Configuration parameters</typeparam>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Set of key/value application configuration properties</param>
        /// <returns>Instance of configuration parameters</returns>
        public static TConfig AddConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, IConfig, new()
        {
            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(config.Name, config);

            //and register it as a service
            services.AddSingleton(config);

            return config;
        }




        /// <summary>
        /// Register HttpContextAccessor
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

       

        /// <summary>
        /// Add custom services
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddServices(this IServiceCollection services)
        {


            // Services
            services.AddScoped<IAirportService, AirportService>();
            services.Decorate<IAirportService, CachedAirportService>();
            //services.AddScoped<IRetardService, RetardService>();
        }

        public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration);
            services.AddHttpClient<ICTeleportHttpClient, CTeleportHttpClient>();
        }

        private static void AddSwaggerDocumentation(SwaggerGenOptions o)
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        }



    }
}
