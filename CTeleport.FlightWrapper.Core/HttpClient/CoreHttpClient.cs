using CTeleport.FlightWrapper.Core.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.HttpClient
{
    public class CoreHttpClient : ICoreHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public CoreHttpClient(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Response<T> Get<T>(string apiMethod, string parameters=null) where T : class
        {
            return GetAsync<T>( apiMethod, parameters).GetAwaiter().GetResult();
        }

        public async Task<Response<T>> GetAsync<T>(string apiMethod, string parameters=null) where T : class
        {
          
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(apiMethod);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return new Response<T>()
                {
                    IsSuccess = httpResponseMessage.IsSuccessStatusCode,
                    Status = httpResponseMessage.StatusCode,
                    

                };
            }

            var strContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(strContent);
            return new Response<T>()
            {
                Data = result,
                IsSuccess = httpResponseMessage.IsSuccessStatusCode,
                Status = httpResponseMessage.StatusCode
            };
        }


    }
}
