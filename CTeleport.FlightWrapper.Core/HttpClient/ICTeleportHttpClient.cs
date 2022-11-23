using CTeleport.FlightWrapper.Core.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.HttpClient
{
    public interface ICTeleportHttpClient
    {
        Response<T> Get<T>(string apiUrl, string apiMethod, string parameters = null) where T : class;
        Task<Response<T>> GetAsync<T>(string apiUrl, string apiMethod, string parameters= null) where T : class;


    }
}
