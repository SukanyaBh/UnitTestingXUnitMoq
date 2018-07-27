using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiSample
{
    public interface ITestProvider
    {
        Task<string> GetTestString(string url);
        Task<HttpResponseMessage> SendAsyncTest();
    }
    public class TestProvider : ITestProvider {

        private HttpClient _httpClient;
        public TestProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetTestString(string url)
        {
            var status = this._httpClient.GetStringAsync(url).IsCompletedSuccessfully;
            if (status)
                return await this._httpClient.GetStringAsync(url);
            else
                return null;
        }

        public async Task<HttpResponseMessage> SendAsyncTest() {
            var result = await this._httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "http://google.com"));
            //var result = await this._httpClient.SendAsync()
            return result;
        }
    }
}
