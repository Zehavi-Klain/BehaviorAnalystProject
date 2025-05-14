using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;

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
        public ActionResult<List<ChildDto>> Get()
        {
            try
            {
                var children = service.GetAll();
                return Ok(children);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
            }
        }

        // GET api/<ChildController>/5
        [HttpGet("{id}")]
        public ActionResult<ChildDto> Get(int id)
        {
            try
            {
                var child = service.GetById(id);
                if (child == null)
                    return NotFound("הילד לא נמצא");
                return Ok(child);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
            }
        }

        // POST api/<ChildController>
        [HttpPost]
        public ActionResult<ChildDto> Post([FromBody] ChildDto child)
        {
            try
            {
                var createdChild = service.AddItem(child);
                return CreatedAtAction(nameof(Get), new { id = createdChild.Code }, createdChild);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"שגיאה בנתונים: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
            }
        }

        // PUT api/<ChildController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ChildDto child)
        {
            try
            {
                service.UpdateItem(id, child);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"שגיאה בנתונים: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
            }
        }

        // DELETE api/<ChildController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
            }
        }
    }
}
