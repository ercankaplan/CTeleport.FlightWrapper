using CTeleport.FlightWrapper.Core.Configuration;
using CTeleport.FlightWrapper.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppSettings _appSettings;
        public AuthService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }


        public bool ValidateApiUser(string appName, string appToken)
        {
            return true;
            //this check is valid for only admin role
            //return (GetApiUser() == new ApiUserModel(){ AppToken = appToken, AppName = appName});

        }
    }
}
