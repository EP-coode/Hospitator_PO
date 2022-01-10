using HospitatorBackend.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HospitatorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestControler : ControllerBase
    {
        private readonly HospitatorDBContext _context;
        public TestControler(HospitatorDBContext context)
        {
            _context = context;
        }
        // GET: api/<TestControler>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TestControler>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var test_data = _context.Kurs.ToList();
            return JsonConvert.SerializeObject(test_data);
        }

        // POST api/<TestControler>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestControler>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestControler>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
