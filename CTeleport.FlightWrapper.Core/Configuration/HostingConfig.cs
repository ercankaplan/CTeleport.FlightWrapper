using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Configuration
{
    /// <summary>
    /// Represents hosting configuration parameters
    /// </summary>
    public partial class HostingConfig : IConfig
    {
        /// <summary>
        /// Gets or sets the airport service url
        /// </summary>
        public string AirportApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the promotion service url
        /// </summary>
        public string PromotionApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the price service url
        /// </summary>
        public string PriceApiUrl { get; set; }

     
    }
}
