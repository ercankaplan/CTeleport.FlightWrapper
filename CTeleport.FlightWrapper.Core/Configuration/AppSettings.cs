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
    }
}
