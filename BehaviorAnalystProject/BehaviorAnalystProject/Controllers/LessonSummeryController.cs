using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Services; // או כל namespace שבו נמצא ה־LessonSummaryService
using System;
using System.Collections.Generic;

namespace BehaviorAnalystProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonSummaryController : ControllerBase
    {
        private readonly IService<LessonSummaryDto> service;

        public LessonSummaryController(IService<LessonSummaryDto> service)
        {
            this.service = service;
        }

        // GET: api/LessonSummary
        [HttpGet]
        public ActionResult<List<LessonSummaryDto>> Get()
        {
            try
            {
                var list = service.GetAll();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
            }
        }

        // GET api/LessonSummary/5
        [HttpGet("{id}")]
        public ActionResult<LessonSummaryDto> Get(int id)
        {
            try
            {
                var summary = service.GetById(id);
                return Ok(summary);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
            }
        }

        // POST api/LessonSummary
        [HttpPost]
        public ActionResult<LessonSummaryDto> Post([FromBody] LessonSummaryDto item)
        {
            try
            {
                var created = service.AddItem(item);
                return CreatedAtAction(nameof(Get), new { id = created.ID }, created);
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

        // PUT api/LessonSummary/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] LessonSummaryDto item)
        {
            try
            {
                service.UpdateItem(id, item);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"שגיאה בנתונים: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
            }
        }

        // DELETE api/LessonSummary/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
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
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
            }
        }
    }
}
