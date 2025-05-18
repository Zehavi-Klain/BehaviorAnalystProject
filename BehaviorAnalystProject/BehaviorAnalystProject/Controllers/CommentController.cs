using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Services;

namespace BehaviorAnalystProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IService<CommentDto> service;

        public CommentController(IService<CommentDto> service)
        {
            this.service = service;
        }

        // GET: api/Comment
        [HttpGet]
        public ActionResult<IEnumerable<CommentDto>> Get()
        {
            var comments = service.GetAll();
            return Ok(comments);
        }

        // GET: api/Comment/5
        [HttpGet("{id}")]
        public ActionResult<CommentDto> Get(int id)
        {
            try
            {
                var comment = service.GetById(id);
                return Ok(comment);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Comment
        [HttpPost]
        public ActionResult<CommentDto> Post([FromBody] CommentDto comment)
        {
            try
            {
                var added = service.AddItem(comment);
                return CreatedAtAction(nameof(Get), new { id = added.ID }, added);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Missing data: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Invalid data: {ex.Message}");
            }
            catch (Exception ex)
            {
                // שגיאה כללית לא צפויה
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }


        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public ActionResult<Comment> Put(int id, [FromBody] CommentDto comment)
        {
            if (id != comment.ID)
                return BadRequest("ID in URL does not match ID in body.");

            try
            {
                var updated = service.UpdateItem(id,comment);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var comment = service.GetById(id);
                service.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
