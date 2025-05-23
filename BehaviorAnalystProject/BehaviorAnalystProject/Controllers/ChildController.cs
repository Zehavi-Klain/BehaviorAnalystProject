using Microsoft.AspNetCore.Mvc;
using Common.Dto;
using Service.Services;

[ApiController]
[Route("api/[controller]")]
public class ChildController : ControllerBase
{
    private readonly IService<ChildDto> _childService;

    public ChildController(IService<ChildDto> childService)
    {
        _childService = childService;
    }

    [HttpGet]
    public ActionResult<List<ChildDto>> GetAll()
    {
        return Ok(_childService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<ChildDto> GetById(int id)
    {
        try
        {
            return Ok(_childService.GetById(id));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public ActionResult<ChildDto> Add([FromBody] ChildDto item)
    {
        try
        {
            var added = _childService.AddItem(item);
            return CreatedAtAction(nameof(GetById), new { id = added.Id }, added);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<ChildDto> Update(int id, [FromBody] ChildDto item)
    {
        try
        {
            return Ok(_childService.UpdateItem(id, item));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _childService.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}/comments")]
    public ActionResult<List<CommentDto>> GetComments(int id)
    {
        try
        {
            var comments = _childService.GetChildComments(id);
            return Ok(comments);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("{id}/lessonSummery")]
    public ActionResult<List<LessonSummaryDto>> GetLessonSummery(int id)
    {
        try
        {
            var summaries = _childService.GetLessonSummery(id);
            return Ok(summaries);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}/forms")]
    public ActionResult<List<FormDto>> GetForms(int id)
    {
        try
        {
            var forms = _childService.GetChildForms(id);
            return Ok(forms);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
