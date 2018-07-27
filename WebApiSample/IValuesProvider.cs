using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiSample
{
    public interface IValuesProvider
    {
        //Task<string> GetStringResult();
        Task<HttpResponseMessage> GetAsync(string url);
        Task<string> GetStringResult(string url);
    }
    public class ValuesProvider : IValuesProvider {
        
        public IHttpHandler _httpHandler;

        //public ValuesProvider(HttpClient httpClient)
        //{
        //    this._httpClient = httpClient;
        //}

        public ValuesProvider(IHttpHandler httpHandler)
        {
            this._httpHandler = httpHandler;
        }
        
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await this._httpHandler.GetAsync("http://google.com");
        }

        public async Task<string> GetStringResult(string url)
        {
            return await this._httpHandler.GetStringAsync(url);
        }
    }
}
