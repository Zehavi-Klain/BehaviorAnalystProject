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

        [HttpGet("ByCategory/{categoryId}")]
        public ActionResult<List<FormDto>> GetByFormCategoryId(int id,int categoryID)
        {
            try
            {
                var form = service.GetFormsByIdCategory(id, categoryID);
                if (form == null)
                    return NotFound($"לא נמצאו טפסים עם קטוגריה {id}."); // אם הטופס לא נמצא
                return Ok(form); // מחזיר את הטופס המבוקש
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}"); // במקרה של שגיאה בלתי צפויה
            }
        }


        // POST api/<FormController>
        [HttpPost]
        public ActionResult<FormDto> Post([FromForm] FormDto form)
        {
            try
            {
                if (form == null)
                    return BadRequest("הבקשה אינה מכילה נתונים.");

                var createdForm = service.AddItem(form);

                return CreatedAtAction(nameof(Get), new { id = createdForm.Id }, createdForm);
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
        private string UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("קובץ לא תקין.");

            var uploadsFolder = Path.Combine(Environment.CurrentDirectory, "Forms");
            Directory.CreateDirectory(uploadsFolder); // מוודא שהתיקייה קיימת

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName; // אפשר לשמור את השם במסד הנתונים אם רוצים
        }
    }
}
