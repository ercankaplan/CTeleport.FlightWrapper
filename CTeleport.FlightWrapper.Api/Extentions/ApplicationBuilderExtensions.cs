using CTeleport.FlightWrapper.Api.Models.Base;
using CTeleport.FlightWrapper.Core.Configuration;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.HttpOverrides;

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
        public static void ConfigureRequestPipeline(this IApplicationBuilder application, AppSettings appSettings)
        {
            application.UseStatusCodePagesWithReExecute("/errors", "?code={0}");
            application.UseSeturExceptionHandler();

            // Configure files
            application.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = false,
                DefaultContentType = "text/plain"
            });


            // Configure Swagger

            //if (app.Environment.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }

           

            // For IP Address
            application.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            // Configure routing
            application.UseRouting();

            // Configure authentication
            application.UseAuthentication();
            application.UseAuthorization();


            application.UseHttpsRedirection();

            //application.MapControllers();


            // Configure Mvc
            //application.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            //});
        }

        /// <summary>
        /// Add exception handling
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseSeturExceptionHandler(this IApplicationBuilder application)
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
