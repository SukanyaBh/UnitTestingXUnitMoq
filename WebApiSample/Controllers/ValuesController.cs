using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiSample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public static IHttpHandler _httpHandler;

        public IValuesProvider _valueProvider = new ValuesProvider(_httpHandler);

        public ValuesController()
        {

        }
        public ValuesController(IValuesProvider valueProvider)
        {
            _valueProvider = valueProvider;
        }

        // GET api/values
        [HttpGet]
        public Task<string> GetStringResult(string url)
        {
            return this._valueProvider.GetStringResult(url);
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
