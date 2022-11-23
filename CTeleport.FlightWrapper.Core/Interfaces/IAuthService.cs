using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Interfaces
{
    public interface IAuthService
    {
        bool ValidateApiUser(string envName, string authToken);
    }
}
