using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace capacita_digital_api.Src.Domains.Sad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SadController : ControllerBase
    {
        // GET: api/<SadController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SadController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
