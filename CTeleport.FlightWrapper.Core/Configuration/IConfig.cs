using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Configuration
{
    /// <summary>
    /// Represents a configuration from app settings
    /// </summary>
    public partial interface IConfig
    {
        /// <summary>
        /// Gets a section name to load configuration
        /// </summary>
        [JsonIgnore]
        string Name => GetType().Name;
    }
}
