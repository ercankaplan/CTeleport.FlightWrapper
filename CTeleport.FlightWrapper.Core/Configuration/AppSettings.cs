using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Configuration
{
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets project configuration parameters
        /// </summary>
        public ProjectConfig ProjectConfig { get; set; } = new ProjectConfig();


        /// <summary>
        /// Gets or sets hosting configuration parameters
        /// </summary>
        public HostingConfig HostingConfig { get; set; } = new HostingConfig();

        /// <summary>
        /// Gets or sets rate limiting configuration on initialization
        /// </summary>
        public RateLimitingConfig RateLimitingConfig { get; set; } = new RateLimitingConfig();

        /// <summary>
        /// Gets or sets Redis configuration parameters
        /// </summary>
        public RedisConfig RedisConfig { get; set; } = new RedisConfig();
    }
}
