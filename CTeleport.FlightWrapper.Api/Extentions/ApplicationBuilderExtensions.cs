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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseErrorHandlingMiddleware();

            app.UseStatusCodePagesWithReExecute("/errors", "?code={0}");

            app.UseCustomExceptionHandler();

            // Configure files
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = false,
                DefaultContentType = "text/plain"
            });


            // Configure Swagger

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

  
           
        }

        /// <summary>
        /// Add exception handling
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseCustomExceptionHandler(this IApplicationBuilder application)
        {
            application.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

#if DEBUG
                await context.Response.WriteAsJsonAsync(new ResponseErrorViewModel(new List<ErrorViewModel> { new ErrorViewModel("500", exception.Message) }));
#else
                await context.Response.WriteAsJsonAsync(new ResponseErrorViewModel(new List<ErrorViewModel> { new ErrorViewModel("500", "A generic error has occurred on the server.") }));
#endif
            }));
        }


    }
}
