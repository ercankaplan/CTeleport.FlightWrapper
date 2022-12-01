using CTeleport.FlightWrapper.Core.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.HttpClient
{
    public interface ICoreHttpClient
    {
        Response<T> Get<T>(string apiMethod, string parameters = null) where T : class;
        Task<Response<T>> GetAsync<T>(string apiMethod, string parameters= null) where T : class;


    }
}
