using System;
using System.Net;
using System.Reflection;
using System.Text.Json;
using CTeleport.FlightWrapper.Core.Exceptions;
using BadRequestException = CTeleport.FlightWrapper.Core.Exceptions.BadRequestException;
using KeyNotFoundException = CTeleport.FlightWrapper.Core.Exceptions.KeyNotFoundException;
using NotImplementedException = CTeleport.FlightWrapper.Core.Exceptions.NotImplementedException;
using UnauthorizedAccessException = CTeleport.FlightWrapper.Core.Exceptions.UnauthorizedAccessException;


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
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            var stackTrace = String.Empty;
            string message;
            var exceptionType = exception.GetType();

            if (exceptionType == typeof(BadRequestException))
            {
                message = exception.Message;
                status = HttpStatusCode.BadRequest;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                message = exception.Message;
                status = HttpStatusCode.NotFound;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                status = HttpStatusCode.NotImplemented;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                status = HttpStatusCode.Unauthorized;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(KeyNotFoundException))
            {
                status = HttpStatusCode.Unauthorized;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }

            var logText = JsonSerializer.Serialize(new { error = message, stackTrace });
            var exceptionResult = JsonSerializer.Serialize(new { errorCode= status,  errorMessage = message });
            

            _logger.LogError(exception, $"{exception.Source} => {logText}");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            context.Response.WriteAsync(exceptionResult);

           
           
        }

        private string GetInnerExceptionMessage(Exception exception)
        {
            if (exception.InnerException != null)
                return GetInnerExceptionMessage(exception.InnerException);

            return exception.Message;
        }

       



    }
}
