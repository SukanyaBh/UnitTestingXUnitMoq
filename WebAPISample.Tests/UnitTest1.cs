using Moq;
using Moq.Protected;
using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebApiSample;
using WebApiSample.Controllers;
using Xunit;

namespace WebAPISample.Tests
{
    public class UnitTest
    {
        [Fact]
        public void TestGet()
        {
            var obj = new ValuesController();
            var res = obj.Get();
            string[] val = { "value1", "value2" };
            Assert.Equal(res, val);
        }

        [Fact]
        public void TestPost() {

        }

        [Fact]
        public async void TestStringResult()
        {
            var requestUri = new Uri("http://google.com");

            var mock = new Mock<IValuesProvider>();

            mock.Setup(x => x.GetStringResult("")).Returns(Task.FromResult("test"));
            ValuesController ctrl = new ValuesController(mock.Object);
            //string result = await ctrl.GetStringResult();
            string result = ctrl.GetStringResult("").Result;

            Assert.NotNull(result);
            Assert.Equal("test", result);
        }

        [Fact]
        public async void TestStringResultHttp()
        {
            var mock = new Mock<IHttpHandler>();
            var url = "http://google.com";
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("test") };
            mock.Setup(x => x.GetStringAsync(url)).Returns(Task.FromResult("test"));
            ValuesProvider provider = new ValuesProvider(mock.Object);
            string res = await provider.GetStringResult(url);
            //string res = provider.GetStringResult(url).Result;
            Assert.NotNull(res);

        }
        /* We cannot mock HttpClient normally. For that we need to use MockHttpMessageHandler */
        /* Mocking HttpClient using MockHttpMessageHandler */
        [Fact]
        public async void Test_Provider_GetTestString() {
            MockHttpMessageHandler mock = new MockHttpMessageHandler();
            mock.When("*").Respond(x => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("test") });
            HttpClient http = new HttpClient();
            http = mock.ToHttpClient();
            TestProvider provider = new TestProvider(http);
            //provider.GetTestString("");
            TestController ctrl = new TestController(provider);
            var result = await ctrl.GetTestStringAsync("http://google.com");
            Assert.Equal("test", result);
        }

        [Fact]
        public async void Test_Provider_GetTestString_From_Provider_Success() {
            string url = "http://google.com";
            MockHttpMessageHandler mock = new MockHttpMessageHandler();
            mock.When("*").Respond(x => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("test") });
            var mockHttp = mock.ToHttpClient();
            TestProvider testProvider = new TestProvider(mockHttp);
            string result = await testProvider.GetTestString(url);
            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_Provider_GetTestString_From_Provider_Exception() {
            string url = "http://google.com";
            MockHttpMessageHandler mock = new MockHttpMessageHandler();
            mock.When("*").Respond(x => new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("test") });
            var mockHttp = mock.ToHttpClient();
            TestProvider testProvider = new TestProvider(mockHttp);
            string result = await testProvider.GetTestString(url);
            Assert.Null(result);

        }
    }
}
