using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BehaviorAnalystProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildController : ControllerBase
    {
        private readonly IService<ChildDto> service;

        public ChildController(IService<ChildDto> service)
        {
            this.service = service;
        }

        // GET: api/<ChildController>
        [HttpGet]
        public List<ChildDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<ChildController>/5
        [HttpGet("{id}")]
        public ChildDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<ChildController>
        [HttpPost]
        public void Post([FromBody] ChildDto child)
        {
            service.AddItem(child);
        }

        // PUT api/<ChildController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ChildDto child)
        {
            service.UpdateItem(id, child);
        }

        // DELETE api/<ChildController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
