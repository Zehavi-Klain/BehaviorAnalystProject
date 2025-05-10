using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Interfaces;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BehaviorAnalystProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalystController : ControllerBase
    {
        private readonly IService<AnalystDto> service;

        public AnalystController(IService<AnalystDto> service)
        {
            this.service = service;
        }

        //     private readonly IService<>
        // GET: api/<AnalystController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AnalystController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        // POST api/<AnalystController>
        [HttpPost]
        public AnalystDto Post([FromBody] AnalystDto analyst)
        {
            return service.AddItem(analyst);

        }

        // PUT api/<AnalystController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AnalystController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}