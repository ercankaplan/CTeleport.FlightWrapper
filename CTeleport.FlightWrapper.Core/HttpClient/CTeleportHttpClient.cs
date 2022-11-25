using CTeleport.FlightWrapper.Core.Configuration;
using CTeleport.FlightWrapper.Core.Domain.Base;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.HttpClient
{
    public class CTeleportHttpClient : CoreHttpClient, ICTeleportHttpClient
    {
        
        public CTeleportHttpClient(IOptions<AppSettings>  appSettings, System.Net.Http.HttpClient httpClient): base(httpClient)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.HostingConfig.AirportApiUrl);

        }


    }
}
