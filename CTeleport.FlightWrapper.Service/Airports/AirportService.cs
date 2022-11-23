using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CTeleport.FlightWrapper.Core.Configuration;
using CTeleport.FlightWrapper.Core.Domain.Airports;
using CTeleport.FlightWrapper.Core.Domain.Base;
using CTeleport.FlightWrapper.Core.Extentions;
using CTeleport.FlightWrapper.Core.HttpClient;
using CTeleport.FlightWrapper.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CTeleport.FlightWrapper.Service.Airports
{
    public class AirportService : IAirportService
    {

        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICTeleportHttpClient _httpClient;
        public AirportService(AppSettings appSettings, IHttpContextAccessor httpContextAccessor, ICTeleportHttpClient httpClient)
        {
            _appSettings = appSettings;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
        }

        public async Task<Response<double>> GetDistance(string orginAirportCode, string destinationAirportCode)
        {
            var orgAirportResponse = await _httpClient.GetAsync<Airport>(_appSettings.HostingConfig.AirportApiUrl, string.Format(AirportServiceDefaults.ApiGet_AirportByIATACode, orginAirportCode));

            if (!orgAirportResponse.IsSuccess)
            {
                return new Response<double>() { IsSuccess = false, Status = orgAirportResponse.Status, Message = "Orgin Airport not found" };
            }
            var destAirportResponse = await _httpClient.GetAsync<Airport>(_appSettings.HostingConfig.AirportApiUrl, string.Format(AirportServiceDefaults.ApiGet_AirportByIATACode, destinationAirportCode));

            if (!destAirportResponse.IsSuccess)
            {
                return new Response<double>() { IsSuccess = false, Status = destAirportResponse.Status, Message = "Destination Airport not found" };
            }

            return new Response<double>
            {
                IsSuccess = true,
                Status = System.Net.HttpStatusCode.OK,
                Data = orgAirportResponse.Data.GetDistanceInMiles(destAirportResponse.Data)
            };

        }
    }
}
