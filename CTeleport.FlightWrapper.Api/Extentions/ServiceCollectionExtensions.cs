using CTeleport.FlightWrapper.Core.Configuration;
using CTeleport.FlightWrapper.Core.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;

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
        public static AppSettings ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            // set culture info
            var culture = new CultureInfo("tr-TR");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            //add configuration parameters
            var appSettings = new AppSettings();
            configuration.Bind(appSettings);
            services.AddSingleton(appSettings);

            Singleton<AppSettings>.Instance = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            Singleton<IWebHostEnvironment>.Instance = environment;

            AppSettings = appSettings;
            Configuration = configuration;

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();


            //add options feature
            services.AddOptions();

            //add caching
            //services.AddCaching();

            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            //add logging
            //services.AddSeturLogger();


            //add data protection
            //services.AddSeturDataProtection();

            //register custom services
            services.AddServices();

            //add swagger
            //services.AddSwagger();

            //add automapper
            //services.AddAutoMapper();

            //add auth
            //services.AddSeturAuthentication();

           

            //add mvc
            //services.AddSeturMvc();

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
            //services.AddScoped<IAzureSearchService, AzureSearchService>();
            //services.AddScoped<IRetardService, RetardService>();
        }

        
    }
}
