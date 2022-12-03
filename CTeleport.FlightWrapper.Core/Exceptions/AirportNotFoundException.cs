using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Exceptions
{
    public class AirportNotFoundException: ArgumentException
    {
       
        public AirportNotFoundException(string message) : base(message)
        { 
           
        }
        public AirportNotFoundException(string message,Exception innerException) : base(message, innerException)
        {

        }
    }
}
