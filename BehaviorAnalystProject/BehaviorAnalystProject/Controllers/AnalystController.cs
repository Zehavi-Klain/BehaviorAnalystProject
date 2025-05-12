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
        public List<AnalystDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<AnalystController>/5
        [HttpGet("{id}")]
        public AnalystDto Get(int id)
        {
            return service.GetById(id);
        }


        // POST api/<AnalystController>
        [HttpPost]
        public AnalystDto Post([FromBody] AnalystDto analyst)
        {
            return service.AddItem(analyst);

        }

        // PUT api/<AnalystController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] AnalystDto analyst)
        {
            service.UpdateItem(id, analyst);
        }

        // DELETE api/<AnalystController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}