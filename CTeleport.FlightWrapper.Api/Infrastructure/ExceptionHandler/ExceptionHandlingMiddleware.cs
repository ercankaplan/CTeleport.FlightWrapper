using System;
using System.Net;
using System.Reflection;

namespace CTeleport.FlightWrapper.Api.Infrastructure.ExceptionHandler
{
    /// <summary>
    /// Handling errors globally with the custom middleware.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="requestDelegate">RequestDelegate type is a function delegate that can process our HTTP requests.</param>
        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate,
             ILogger<ExceptionHandlingMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        /// <summary>
        ///  if a request is unsuccessful middleware will trigger the catch block and call the HandleExceptionAsync method.
        /// </summary>
        /// <param name="context">HttpContext.</param>
        /// <returns>Task.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (ApplicationException ax)
            {
                await HandleExceptionAsync(context, ax);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        ///  Sets up the response status code and content type and return a response.
        /// </summary>
        /// <param name="context">HttpContext.</param>
        /// <param name="ex">Exception.</param>
        /// <returns>Task.</returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {


            var innerExMessage = GetInnerExceptionMessage(ex);

            _logger.LogError(ex, $"{ex.Source} => {innerExMessage}");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(ex.Message);
           
        }

        private string GetInnerExceptionMessage(Exception exception)
        {
            if (exception.InnerException != null)
                return GetInnerExceptionMessage(exception.InnerException);

            return exception.Message;
        }

       



    }
}
