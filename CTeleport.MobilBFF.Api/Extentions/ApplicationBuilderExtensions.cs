using AspNetCoreRateLimit;
using CTeleport.FlightWrapper.Core.Configuration;
using CTeleport.MobilBFF.Api.ExceptionHandler;

namespace CTeleport.MobilBFF.Api.Extentions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureRequestPipeline(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseExceptionHandlingMiddleware();

            //app.UseHttpsRedirection();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});


            //app.UseIpRateLimiting();

            // Configure Swagger

            //if (env.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}
        }

        public static void UseExceptionHandlingMiddleware(this IApplicationBuilder application)
        {
            application.UseMiddleware<ExceptionHandlingMiddleware>();

        }
    }
}
