using CTeleport.FlightWrapper.Core.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Interfaces
{
    public interface IAirportService
    {
        public Task<Response<double>> GetDistance(string orginAirportCode, string destinationAirportCode);
    }
}
