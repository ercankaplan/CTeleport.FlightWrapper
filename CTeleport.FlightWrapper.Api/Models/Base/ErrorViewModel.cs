using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CTeleport.FlightWrapper.Api.Models.Base
{
    /// <summary>
    /// Represents a response error
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets or sets the detail
        /// </summary>
        public string Detail { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="title">Field</param>
        /// <param name="detail">Message</param>
        public ErrorViewModel(string title, string detail)
        {
            Title = title != string.Empty ? title : null;
            Detail = detail;
        }
    }

    /// <summary>
    /// Represents a response error model
    /// </summary>
    public class ResponseErrorViewModel
    {
        /// <summary>
        /// Gets the error list
        /// </summary>
        public List<ErrorViewModel> Errors { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public ResponseErrorViewModel(List<ErrorViewModel> errors)
        {
            Errors = errors;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="modelState">Model state dictionary</param>
        public ResponseErrorViewModel(ModelStateDictionary modelState)
        {
            Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ErrorViewModel(key, x.ErrorMessage)))
                    .ToList();
        }
    }
}
