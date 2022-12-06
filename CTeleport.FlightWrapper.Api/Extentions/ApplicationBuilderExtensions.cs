using AspNetCoreRateLimit;
using CTeleport.FlightWrapper.Api.Infrastructure.ExceptionHandler;
using CTeleport.FlightWrapper.Api.Models.Base;
using CTeleport.FlightWrapper.Core.Configuration;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;

namespace CTeleport.FlightWrapper.Api.Extentions
{
    /// <summary>
    /// Represents extensions of IApplicationBuilder
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        /// <param name="appSettings">Settings of the application</param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder app , IWebHostEnvironment env, AppSettings appSettings)
        {


            // Configure the HTTP request pipeline.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandlingMiddleware();

            app.UseHttpsRedirection();

            //app.UseRouting();

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});


            app.UseIpRateLimit();

            // Configure Swagger

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

  
           
        }

        private static void UseIpRateLimit(this IApplicationBuilder app)
        {

            app.UseIpRateLimiting();
        }

        /// <summary>
        /// Add exception handling
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseExceptionHandlingMiddleware(this IApplicationBuilder application)
        {
            application.UseMiddleware<ExceptionHandlingMiddleware>();

        }


    }
}
