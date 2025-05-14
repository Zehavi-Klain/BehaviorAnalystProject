using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;

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

        // GET: api/<AnalystController>
        [HttpGet]
        public ActionResult<List<AnalystDto>> Get()
        {
            try
            {
                var analysts = service.GetAll();
                return Ok(analysts); // מחזיר את רשימת האנליסטים
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}"); // במקרה של שגיאה בלתי צפויה
            }
        }

        // GET api/<AnalystController>/5
        [HttpGet("{id}")]
        public ActionResult<AnalystDto> Get(int id)
        {
            try
            {
                var analyst = service.GetById(id);
                if (analyst == null)
                    return NotFound("האנליסט לא נמצא");
                return Ok(analyst); // מחזיר את האנליסט המבוקש
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
            }
        }

        // POST api/<AnalystController>
        [HttpPost]
        public ActionResult<AnalystDto> Post([FromBody] AnalystDto analyst)
        {
            try
            {
                var createdAnalyst = service.AddItem(analyst);
                return CreatedAtAction(nameof(Get), new { id = createdAnalyst.Code }, createdAnalyst);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"שגיאה בנתונים: {ex.Message}"); // אם יש שגיאה בתוקף הנתונים
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
            }
        }

        // PUT api/<AnalystController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] AnalystDto analyst)
        {
            try
            {
                service.UpdateItem(id, analyst);
                return NoContent(); // הצלחה ללא תוכן
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"שגיאה בנתונים: {ex.Message}"); // אם יש שגיאה בתוקף הנתונים
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
            }
        }

        // DELETE api/<AnalystController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Delete(id);
                return NoContent(); // הצלחה ללא תוכן
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"לא נמצא אנליסט עם מזהה {id}."); // אם האנליסט לא נמצא
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
            }
        }
    }
}
