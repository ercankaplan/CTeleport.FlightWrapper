using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Domain.Base
{
    /// <summary>
    /// Response interface
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Gets or sets the response status
        /// </summary>
        HttpStatusCode Status { get; set; }

        /// <summary>
        /// Gets or sets the response message
        /// </summary>
        string Message { get; set; }
    }

    /// <summary>
    /// Generic response interface
    /// </summary>
    public interface IResponse<T> : IResponse
    {
        /// <summary>
        /// Gets or sets the data
        /// </summary>
        T Data { get; set; }
    }
}
