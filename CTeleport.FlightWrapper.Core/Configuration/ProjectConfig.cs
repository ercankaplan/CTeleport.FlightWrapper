using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Configuration
{
    /// <summary>
    /// Represents project configuration parameters
    /// </summary>
    public partial class ProjectConfig : IConfig
    {
        /// <summary>
        /// Gets or sets the project token
        /// </summary>
        public string ProjectToken { get; set; }

        /// <summary>
        /// Gets or sets the project id
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        /// Gets or sets the project email group
        /// </summary>
        
    }
}
