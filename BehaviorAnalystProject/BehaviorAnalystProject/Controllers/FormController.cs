using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public ActionResult<List<FormDto>> Get()
        {
            try
            {
                var forms = service.GetAll();
                return Ok(forms); // מחזיר את רשימת הטפסים
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}"); // במקרה של שגיאה בלתי צפויה
            }
        }

        // GET api/<FormController>/5
        [HttpGet("{id}")]
        public ActionResult<FormDto> Get(int id)
        {
            try
            {
                var form = service.GetById(id);
                if (form == null)
                    return NotFound($"לא נמצא טופס עם מזהה {id}."); // אם הטופס לא נמצא
                return Ok(form); // מחזיר את הטופס המבוקש
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}"); // במקרה של שגיאה בלתי צפויה
            }
        }

        // POST api/<FormController>
        [HttpPost]
        public ActionResult<FormDto> Post([FromBody] FormDto form)
        {
            try
            {
                if (form == null)
                    return BadRequest("הבקשה אינה מכילה נתונים."); // אם הטופס הוא null
                var createdForm = service.AddItem(form);
                return CreatedAtAction(nameof(Get), new { id = createdForm.Id }, createdForm); // מחזיר את הטופס שנוצר
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"שגיאה בנתונים: {ex.Message}"); // אם יש שגיאה בתוקף הנתונים
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}"); // במקרה של שגיאה בלתי צפויה
            }
        }

        // PUT api/<FormController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] FormDto form)
        {
            try
            {
                if (form == null)
                    return BadRequest("הבקשה אינה מכילה נתונים.");
                service.UpdateItem(id, form);
                return NoContent(); // הצלחה ללא תוכן
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"שגיאה בנתונים: {ex.Message}"); // אם יש שגיאה בתוקף הנתונים
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}"); // במקרה של שגיאה בלתי צפויה
            }
        }

        // DELETE api/<FormController>/5
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
                return NotFound($"לא נמצא טופס עם מזהה {id}."); // אם הטופס לא נמצא
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}"); // במקרה של שגיאה בלתי צפויה
            }
        }
    }
}
