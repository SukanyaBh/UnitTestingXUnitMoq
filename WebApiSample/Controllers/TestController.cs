using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiSample.Controllers
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        public static HttpClient _httpClient;
        ITestProvider _testProvider = new TestProvider(_httpClient);
        public TestController(TestProvider testProvider)
        {
            _testProvider = testProvider;
        }

        [HttpGet]
        public Task<string> GetTestStringAsync(string url)
        {
            return this._testProvider.GetTestString(url);
        }

        [HttpGet]
        public Task<HttpResponseMessage> SendAsyncTest() {

        }
    }
}