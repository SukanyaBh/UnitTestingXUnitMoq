using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiSample
{
    public interface IHttpHandler
    {
        HttpResponseMessage Get(string url);
        HttpResponseMessage Post(string url, HttpContent content);
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
        Task<string> GetStringAsync(string url);
    }

    public class HttpClientHandler: IHttpHandler
    {
        private HttpClient _httpClient;
        public HttpClientHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpResponseMessage Get(string url)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            //var res = this._httpClient.GetAsync(url);
            return this._httpClient.GetAsync(url);
        }

        public async Task<string> GetStringAsync(string url)
        {
            return await this._httpClient.GetStringAsync(url);
        }

        public HttpResponseMessage Post(string url, HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return this._httpClient.PostAsync(url, content);
        }
    }
}
