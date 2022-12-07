using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Configuration
{

    public partial class RateLimitingConfig : IConfig
    {
        public bool Enable { get; set; }
    }
}
