using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Services;
using System;
using System.Collections.Generic;

namespace BehaviorAnalystProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormCategoryController : ControllerBase
    {
        private readonly FormCategoryService service;

        public FormCategoryController(FormCategoryService service)
        {
            this.service = service;
        }

        // GET: api/FormCategory
        [HttpGet]
        public ActionResult<List<FormCategory>> Get()
        {
            try
            {
                var categories = service.GetAll();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/FormCategory/5
        [HttpGet("{id}")]
        public ActionResult<FormCategory> Get(int id)
        {
            try
            {
                var category = service.GetById(id);
                return Ok(category);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/FormCategory
        [HttpPost]
        public ActionResult<FormCategory> Post([FromBody] FormCategory item)
        {
            try
            {
                var added = service.AddItem(item);
                return CreatedAtAction(nameof(Get), new { id = added.Code }, added);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/FormCategory/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FormCategory item)
        {
            try
            {
                service.UpdateItem(id, item);
                return NoContent(); // 204
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/FormCategory/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                service.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
