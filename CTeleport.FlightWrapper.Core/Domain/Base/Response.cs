using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Domain.Base
{
    /// <summary>
    /// Represents a response
    /// </summary>
    public partial class Response<T> //: IResponse<T>
    {
        /// <summary>
        /// Gets or sets the response IsSuccess
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Gets or sets the response status
        /// </summary>
        public HttpStatusCode Status { get; set; }

        /// <summary>
        /// Gets or sets the response message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the ResponseT class without input.
        /// </summary>
        public Response()
            : this(HttpStatusCode.OK)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ResponseT class with a response status.
        /// </summary>
        /// <param name="status">Response Status</param>
        public Response(HttpStatusCode status)
            : this(status, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ResponseT class with a response status and message.
        /// </summary>
        /// <param name="status">Response Status</param>
        /// <param name="message">Message</param>
        public Response(HttpStatusCode status, string message)
            : this(status, message, default(T))
        {
        }

        /// <summary>
        /// Initializes a new instance of the ResponseT class with a response status, message and data.
        /// </summary>
        /// <param name="status">Response Status</param>
        /// <param name="message">Message</param>
        /// <param name="data">Data</param>
        public Response(HttpStatusCode status, string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
