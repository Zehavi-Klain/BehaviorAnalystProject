using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BehaviorAnalystProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IService<FormDto> service;

        public FormController(IService<FormDto> service)
        {
            this.service = service;
        }
        // GET: api/<FormController>
        [HttpGet]
        public List<FormDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<FormController>/5
        [HttpGet("{id}")]
        public FormDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<FormController>
        [HttpPost]
        public void Post([FromBody] FormDto form)
        {
            service.AddItem(form);
        }

        // PUT api/<FormController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] FormDto form)
        {
            service.UpdateItem(id, form);
        }

        // DELETE api/<FormController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
